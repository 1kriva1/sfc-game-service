using System.ComponentModel;

namespace SFC.Game.Domain.Enums.Metadata;
public enum MetadataState
{
    [Description("Not Required")]
    NotRequired,
    Required,
    Done
}