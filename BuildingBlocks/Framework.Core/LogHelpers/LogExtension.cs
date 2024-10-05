using System.Text.Json;
using System.Text.Json.Nodes;
using Framework.Core.DomainObjects;
using Microsoft.Extensions.Logging;

namespace Framework.Core.LogHelpers;

public static class LogExtension{
    public static void CreateLog(this ILogger logger, GenericLog genericLog ){
            var json = JsonSerializer.Serialize(genericLog);

            logger.LogInformation(json);
    }

      public static void CreateLog(this ILogger logger, ResponseLog genericLog ){
            var json = JsonSerializer.Serialize(genericLog);

            logger.LogInformation(json);
    }

    public static void CreateLog(this ILogger logger, CriticErroLog errorLog ){

            var json = JsonSerializer.Serialize(errorLog);

            logger.LogCritical(json);
    }
}


public record GenericLog(CorrelationId CorrelationId, string MessageType, string[] TypeLog, object message);
public record CriticErroLog(CorrelationId CorrelationId, string MessageType, string[] TypeLog, object message);

public record ResponseLog(CorrelationId CorrelationId, string MessageType, string[] TypeLog, object message,long ElapsedMilliseconds);
