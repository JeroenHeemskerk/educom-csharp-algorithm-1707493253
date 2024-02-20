using NUnit.Framework;
using Organizer;

namespace BornToMove.OrganizerTest {
    [TestFixture]
    public class RotateSortTest {
        [Test]
        public void testSortEmpty() {
            RotateSort<int> sorter = new RotateSort<int>();
            List<int> input = new List<int>();
            IComparer<int> comp = Comparer<int>.Default;

            var result = sorter.Sort(input, comp);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Exactly(0).Items);
            Assert.That(result, Is.EquivalentTo(new int[] { }));
            // also check that our input is not modified
            Assert.That(input, Is.EquivalentTo(new int[] { }));
        }

        [Test]
        public void testSortOneElement() {
            RotateSort<int> sorter = new RotateSort<int>();
            List<int> input = new List<int>() {1};
            IComparer<int> comp = Comparer<int>.Default;

            var result = sorter.Sort(input, comp);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Exactly(1).Items);
            Assert.That(result, Is.EquivalentTo(new int[] {1}));
            // also check that our input is not modified
            Assert.That(input, Is.EquivalentTo(new int[] {1}));
        }

        [Test]
        public void testSortTwoElements() {
            RotateSort<int> sorter = new RotateSort<int>();
            List<int> input = new List<int>() { 24, 11 };
            IComparer<int> comp = Comparer<int>.Default;

            var result = sorter.Sort(input, comp);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Exactly(2).Items);
            Assert.That(result, Is.EquivalentTo(new int[] { 11, 24 }));
            // also check that our input is not modified
            Assert.That(input, Is.EquivalentTo(new int[] { 24, 11 }));
        }

        [Test]
        public void testSortThreeEqual() {
            RotateSort<int> sorter = new RotateSort<int>();
            List<int> input = new List<int>() { 16, 16, 16 };
            IComparer<int> comp = Comparer<int>.Default;

            var result = sorter.Sort(input, comp);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Exactly(3).Items);
            Assert.That(result, Is.EquivalentTo(new int[] { 16, 16, 16 }));
            // also check that our input is not modified
            Assert.That(input, Is.EquivalentTo(new int[] { 16, 16, 16 }));
        }

        [Test]
        public void testSortUnsortedArray() {
            RotateSort<int> sorter = new RotateSort<int>();
            List<int> input = new List<int>() { 16, -123, 0 };
            IComparer<int> comp = Comparer<int>.Default;

            var result = sorter.Sort(input, comp);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Exactly(3).Items);
            Assert.That(result, Is.EquivalentTo(new int[] { -123, 0, 16 }));
            // also check that our input is not modified
            Assert.That(input, Is.EquivalentTo(new int[] { 16, -123, 0 }));
        }

        [Test]
        public void testSortUnsortedArrayThreeEqual() {
            RotateSort<int> sorter = new RotateSort<int>();
            List<int> input = new List<int>() { 16, -123, 0, -123, 89, -123, -3435 };
            IComparer<int> comp = Comparer<int>.Default;

            var result = sorter.Sort(input, comp);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Exactly(7).Items);
            Assert.That(result, Is.EquivalentTo(new int[] { -3435, -123, -123, -123, 0, 16, 89 }));
            // also check that our input is not modified
            Assert.That(input, Is.EquivalentTo(new int[] { 16, -123, 0, -123, 89, -123, -3435 }));
        }
    }
}
