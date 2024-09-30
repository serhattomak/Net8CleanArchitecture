using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace App.Repositories.Interceptors;

public class AuditDbContextInterceptor : SaveChangesInterceptor
{
	private static readonly Dictionary<EntityState, Action<DbContext, IAuditEntity>> Behaviours = new()
	{
		{EntityState.Added, AddBehaviour},
		{EntityState.Modified, ModifiedBehaviour}
	};
	private static void AddBehaviour(DbContext context, IAuditEntity auditEntity)
	{
		auditEntity.Created = DateTime.UtcNow;
		context.Entry(auditEntity).Property(x => x.Updated).IsModified = false;
	}

	private static void ModifiedBehaviour(DbContext context, IAuditEntity auditEntity)
	{
		context.Entry(auditEntity).Property(x => x.Created).IsModified = false;
		auditEntity.Updated = DateTime.UtcNow;
	}

	public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
		CancellationToken cancellationToken = new CancellationToken())
	{
		foreach (var entityEntry in eventData.Context!.ChangeTracker.Entries().ToList())
		{
			if (entityEntry.Entity is not IAuditEntity auditEntity) continue;

			Behaviours[entityEntry.State](eventData.Context, auditEntity);

			#region 1st way

			//switch (entityEntry.State)
			//{
			//	case EntityState.Added:
			//		AddBehaviour(eventData.Context, auditEntity);
			//		break;

			//	case EntityState.Modified:
			//		ModifiedBehaviour(eventData.Context, auditEntity);
			//		break;
			//}

			#endregion
		}

		return base.SavingChangesAsync(eventData, result, cancellationToken);
	}
}