// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace hBuddyApp.Client.Services.Platform.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class ArticleBody
    {
        /// <summary>
        /// Initializes a new instance of the ArticleBody class.
        /// </summary>
        public ArticleBody()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ArticleBody class.
        /// </summary>
        public ArticleBody(string text = default(string), string format = default(string))
        {
            Text = text;
            Format = format;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "format")]
        public string Format { get; set; }

    }
}
