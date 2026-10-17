using System.Text.Json.Serialization;

namespace Sndr.Client.Domains;

/// <summary>
/// The region a sending domain is hosted in.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<DomainRegion>))]
public enum DomainRegion
{
    /// <summary>
    /// US East (<c>us-east-1</c>).
    /// </summary>
    [JsonStringEnumMemberName("us-east-1")]
    UsEast1,

    /// <summary>
    /// EU West (<c>eu-west-1</c>).
    /// </summary>
    [JsonStringEnumMemberName("eu-west-1")]
    EuWest1,

    /// <summary>
    /// Asia Pacific Southeast (<c>ap-southeast-1</c>).
    /// </summary>
    [JsonStringEnumMemberName("ap-southeast-1")]
    ApSoutheast1
}