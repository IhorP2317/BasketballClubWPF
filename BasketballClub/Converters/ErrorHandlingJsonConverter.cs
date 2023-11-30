using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BasketballClub.Exceptions {
    public class ErrorHandlingJsonConverter : JsonConverter {
        public override bool CanRead => true;
        public override bool CanWrite => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            JObject jObject = JObject.Load(reader);
            

            if (jObject["status"] != null) {
                int statusCode = jObject["status"].Value<int>();

                if (statusCode >= 400) {
                    var errorDetails = jObject.ToObject<ApiErrorDetails>();
                    throw new ApiException(errorDetails.Title, statusCode);
                }
            }

            return jObject.ToObject(objectType);
        }
        //public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
        //    JObject jObject = JObject.Load(reader);

        //    if (jObject["status"] != null) {
        //        int statusCode = jObject["status"].Value<int>();

        //        if (statusCode >= 400) {
        //            var errorDetails = jObject.ToObject<ApiErrorDetails>();
        //            throw new ApiException(errorDetails.Title, statusCode);
        //        }
        //    }

        //    // Handle the case where "position" is not a string
        //    if (jObject.TryGetValue("position", out JToken positionToken) && positionToken.Type == JTokenType.String) {
        //        return jObject.ToObject(objectType);
        //    }

        //    // Handle other cases or throw an exception if needed
        //    throw new JsonException("Unexpected data type for 'position' property.");

        //}

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType) {
            return true;
        }
    }
}
