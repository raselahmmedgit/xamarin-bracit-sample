// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace hBuddyApp.Client.Services.Platform.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class UtilityArticle
    {
        /// <summary>
        /// Initializes a new instance of the UtilityArticle class.
        /// </summary>
        public UtilityArticle()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the UtilityArticle class.
        /// </summary>
        public UtilityArticle(string languageCode = default(string), string title = default(string), UtilityArticleBody body = default(UtilityArticleBody), ImageLink imageLink = default(ImageLink))
        {
            LanguageCode = languageCode;
            Title = title;
            Body = body;
            ImageLink = imageLink;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "languageCode")]
        public string LanguageCode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "body")]
        public UtilityArticleBody Body { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "imageLink")]
        public ImageLink ImageLink { get; set; }

    }
}
