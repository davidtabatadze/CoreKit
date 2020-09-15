using Newtonsoft.Json;

namespace CoreKit.Extension.Class
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
                // Returning input
                return source;
            }
            // Serialize object to json
            var json = JsonConvert.SerializeObject(source);
            // Deserialize as fresh copy
            return JsonConvert.DeserializeObject<T>(json);
        }

    }

}
