using Hangfire.Client;
using Hangfire.Common;
using Hangfire.Logging;
using Hangfire.Server;
using Hangfire.States;
using Hangfire.Storage;

namespace SFC.Game.Infrastructure.Filters.Hangfire;
public sealed class LogJobAttribute : JobFilterAttribute, IClientFilter, IServerFilter, IApplyStateFilter
{
    private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

    public void OnCreating(CreatingContext context)
    {
        Logger.InfoFormat("Creating a job based on method `{0}`...", context.Job.Method.Name);
    }

    public void OnCreated(CreatedContext context)
    {
        Logger.InfoFormat(
            "Job that is based on method `{0}` has been created with id `{1}`",
            context.Job.Method.Name,
            context.BackgroundJob?.Id);
    }

    public void OnPerforming(PerformingContext context)
    {
        Logger.InfoFormat("Starting to perform job `{0}`", context.BackgroundJob.Id);
    }

    public void OnPerformed(PerformedContext context)
    {
        Logger.InfoFormat("Job `{0}` has been performed", context.BackgroundJob.Id);
    }

    public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
    {
        if (context.NewState is FailedState failedState)
        {
            Logger.ErrorException(
                $"Background job #{context.BackgroundJob.Id} was failed with an exception.",
                failedState.Exception);
        }
    }

    public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction) { }
}