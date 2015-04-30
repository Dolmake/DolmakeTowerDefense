using UnityEngine;
using System.Collections;

namespace SGame.Common
{
	public interface IRange {

		float RangeDistance { get; }
		event System.Action<Entity> OnEntityInRange;
	}
}
