using UnityEngine;
using Verse;

namespace CompactWorkTab
{
    [StaticConstructorOnStartup]
    public static class Textures
    {
        public static readonly Texture2D SortingIcon = ContentFinder<Texture2D>.Get("UI/Icons/Sorting");
        public static readonly Texture2D SortingDescendingIcon = ContentFinder<Texture2D>.Get("UI/Icons/SortingDescending");
    }
}
