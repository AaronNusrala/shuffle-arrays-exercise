using System;
using NUnit.Framework;
using ShuffleArrays;

namespace ShuffleArraysTests
{
	public class Tests
	{
		private ArrayShuffler _shuffler;

		[SetUp]
		public void Setup()
		{
			_shuffler = new ArrayShuffler();
		}

		[Test]
		public void Null_A_Array_Throws_Exception()
		{
			int[] a = null;
			var b = new int[0];

			Assert.Throws<ArgumentException>(() => _shuffler.Shuffle(a, b));
		}

		[Test]
		public void Null_B_Array_Throws_Exception()
		{
			var a = new int[0];
			int[] b = null;

			Assert.Throws<ArgumentException>(() => _shuffler.Shuffle(a, b));
		}

		[Test]
		public void Both_Arrays_Null_Throws_Exception()
		{
			Assert.Throws<ArgumentException>(() => _shuffler.Shuffle(null, null));
		}

		[Test]
		public void Shuffle_Empty_Arrays()
		{
			var a = new int[0];
			var b = new int[0];

			var result = _shuffler.Shuffle(a, b);

			Assert.AreEqual(0, result.Length);
		}

		[Test]
		public void Shuffle_Empty_A_Array_With_Populated_B_Array()
		{
			var a = new int[0];
			var b = new[] { 1 };

			var result = _shuffler.Shuffle(a, b);

			AssertSequenceEqual(new[] { 1 }, result);
		}

		[Test]
		public void Shuffle_Empty_B_Array_With_Populated_A_Array()
		{
			var a = new int[0];
			var b = new[] { 1 };

			var result = _shuffler.Shuffle(a, b);

			AssertSequenceEqual(new[] { 1 }, result);
		}

		[Test]
		public void Shuffle_Arrays_With_Single_Value()
		{
			var a = new[] { 1 };
			var b = new[] { 2 };

			var result = _shuffler.Shuffle(a, b);

			AssertSequenceEqual(new[] { 1, 2 }, result);
		}

		[Test]
		public void Shuffle_Longer_A_Array_With_Shorter_B_Array()
		{
			var a = new[] { 1, 3, 4 };
			var b = new[] { 2 };

			var result = _shuffler.Shuffle(a, b);

			AssertSequenceEqual(new[] { 1, 2, 3, 4 }, result);
		}

		[Test]
		public void Shuffle_Longer_B_Array_With_Shorter_A_Array()
		{
			var a = new[] { 1, 3 };
			var b = new[] { 2, 4, 5 };

			var result = _shuffler.Shuffle(a, b);

			AssertSequenceEqual(new[] { 1, 2, 3, 4, 5 }, result);
		}

		[Test]
		public void Shuffle_Equal_Length_Arrays()
		{
			var a = new[] { 1, 3, 5 };
			var b = new[] { 2, 4, 6 };

			var result = _shuffler.Shuffle(a, b);

			AssertSequenceEqual(new[] { 1, 2, 3, 4, 5, 6 }, result);
		}

		[Test]
		public void Shuffle_Random_Numbers()
		{
			var a = new[] { 222, 345, 23, 452, 2, 442, 9878 };
			var b = new[] { 123, 523, 234, 2, 345, 234, 5, 2345, 23, 45, 234, 5, 2, 23, 45 };

			var result = _shuffler.Shuffle(a, b);

			var shuffled = new[] { 222, 123, 345, 523, 23, 234, 452, 2, 2, 345, 442, 234, 9878, 5, 2345, 23, 45, 234, 5, 2, 23, 45 };
			AssertSequenceEqual(result, shuffled);
		}

		private void AssertSequenceEqual(int[] a, int[] b)
		{
			if (a == null && b != null)
			{
				Assert.Fail("Expected array, got null");
			}

			Assert.AreEqual(a.Length, b.Length, $"Expected {a.Length} elements, got {b.Length}");

			for (var i = 0; i < a.Length; i++)
			{
				Assert.AreEqual(a[i], b[i], $"Expected {a[i]} at index {i}, got {b[i]}");
			}
		}
	}
}