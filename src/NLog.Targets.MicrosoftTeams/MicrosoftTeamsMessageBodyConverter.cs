using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NLog.Targets.MicrosoftTeams
{
    /// <summary>
    /// Ensures derived types of <see cref="MicrosoftTeamsMessageBody"/> are serialized
    /// using their runtime type so subclass properties are included in the output.
    /// </summary>
    internal sealed class MicrosoftTeamsMessageBodyConverter : JsonConverter<MicrosoftTeamsMessageBody>
    {
        public override MicrosoftTeamsMessageBody Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => throw new NotSupportedException();

        public override void Write(Utf8JsonWriter writer, MicrosoftTeamsMessageBody value, JsonSerializerOptions options)
            => JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
