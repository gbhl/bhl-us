public class TSVConverter
{
    public static List<EntityIdentifierPair> ConvertTSVToObjects(string entityType, string tsvContent)
    {
        var result = new List<EntityIdentifierPair>();
        var lines = tsvContent.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        if (lines.Length < 2)
            return result;

        // Parse headers (remove ? prefix and replace underscores with spaces)
        var headers = lines[0].Split('\t');
        for (int i = 0; i < headers.Length; i++)
        {
            headers[i] = headers[i].Trim().TrimStart('?').Replace("_", " ");
        }

        for (int i = 1; i < lines.Length; i++)
        {
            var fields = lines[i].Split('\t');
            var entityID = fields[0].Trim();

            // Skip if the entityID is blank (likely a deprecated BHL ID)
            if (!string.IsNullOrWhiteSpace(entityID))
            {
                for (int j = 1; j < fields.Length; j++)
                {
                    var value = fields[j].Trim();
                    if (!string.IsNullOrEmpty(value))
                    {
                        var newPair = new EntityIdentifierPair
                        {
                            EntityType = entityType,
                            EntityID = entityID,
                            Type = headers[j],
                            Value = value
                        };

                        // Ignore duplicates
                        if (!ContainsMatchingPair(result, newPair)) result.Add(newPair);
                    }
                }
            }
        }

        return result;
    }

    static bool ContainsMatchingPair(List<EntityIdentifierPair> list, EntityIdentifierPair target)
    {
        return list.Any(item =>
            string.Equals(item.EntityType, target.EntityType, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(item.EntityID, target.EntityID, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(item.Type, target.Type, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(item.Value, target.Value, StringComparison.OrdinalIgnoreCase));
    }
}
