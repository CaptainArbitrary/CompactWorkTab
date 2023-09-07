using UnityEngine;
using Verse;

namespace CompactWorkTab
{
    [StaticConstructorOnStartup]
    public static class Textures
    {
        public static readonly Texture2D InclinedTexture = ContentFinder<Texture2D>.Get("CompactWorkTab_Inclined");
        public static readonly Texture2D VerticalTexture = ContentFinder<Texture2D>.Get("CompactWorkTab_Vertical");
        public static readonly Texture2D HorizontalTexture = ContentFinder<Texture2D>.Get("CompactWorkTab_Horizontal");

        public static readonly Texture2D SortingIcon = ContentFinder<Texture2D>.Get("UI/Icons/Sorting");
        public static readonly Texture2D SortingDescendingIcon = ContentFinder<Texture2D>.Get("UI/Icons/SortingDescending");
    }
}
