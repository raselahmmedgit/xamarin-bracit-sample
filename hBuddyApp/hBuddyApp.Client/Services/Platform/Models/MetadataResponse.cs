// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace hBuddyApp.Client.Services.Platform.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class MetadataResponse
    {
        /// <summary>
        /// Initializes a new instance of the MetadataResponse class.
        /// </summary>
        public MetadataResponse()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the MetadataResponse class.
        /// </summary>
        public MetadataResponse(Metadata metadata = default(Metadata))
        {
            Metadata = metadata;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "metadata")]
        public Metadata Metadata { get; set; }

    }
}