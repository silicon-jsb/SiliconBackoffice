using System.Text.Json;

namespace NewBackoffice.Data.GraphQL;

public class DynamicGraphQLResponse
{
    public JsonElement Data { get; set; }
}
