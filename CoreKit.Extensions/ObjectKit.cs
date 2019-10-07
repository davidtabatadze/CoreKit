using Newtonsoft.Json;

namespace CoreKit.Extensions.Class
{

    /// <summary>
    /// Represents an extension for objects
    /// </summary>
    public static class ObjectKit
    {

        /// <summary>
        /// Gets fresh copy of any object
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="source">Source object</param>
        /// <returns>Fresh copy of object</returns>
        public static T Clone<T>(this T source)
        {
            // In case if null
            if (source == null)
            {
                // Returning default
                return default(T);
            }
            // Serialize objrct to json
            var json = JsonConvert.SerializeObject(source);
            // Deserialize as fresh copy
            return JsonConvert.DeserializeObject<T>(json);
        }

    }

}
