using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace CLI_assign.Utils
{
    public class NullableConverter : JsonConverter<double?>
    {
        public override double? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null; 
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                string stringValue = reader.GetString();

                // empty string
                if (string.IsNullOrWhiteSpace(stringValue))
                {
                    return null;
                }

                // string to double value
                if (double.TryParse(stringValue, out double result))
                {
                    return result;
                }

                throw new JsonException($"Cannot convert string '{stringValue}' to double.");
            }

            if (reader.TokenType == JsonTokenType.Number)
            {
                return reader.GetDouble(); //////////////////////////////////
            }

            throw new JsonException($"Cannot convert {reader.TokenType} to double.");
        }

        public override void Write(Utf8JsonWriter writer, double? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteNumberValue(value.Value); 
            }
            else
            {
                writer.WriteNullValue(); 
            }
        }

    }
}
