using UnityEngine;

static class Tools
{
	public static bool IsLayerWithinMask(int layer, LayerMask mask)
	{
		return (mask & (1 << layer)) > 0;
	}
}