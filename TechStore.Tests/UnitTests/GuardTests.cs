using TechStore.Core.Exceptions;

namespace TechStore.Tests.UnitTests
{
	[TestFixture]
	public class GuardTests
	{
		private IGuard guard;
		private string message;

		[OneTimeSetUp]
		public void SetUp()
		{
			this.guard = new Guard();
			this.message = "Error message";
		}

		[Test]
		public void AgainstInvalidUserId_ShouldThrowAPCShopExceptionWhenTheGivenValueIsNull()
		{
			Assert.Throws<TechStoreException>(() => this.guard.AgainstInvalidUserId<object?>(null));
		}

		[Test]
		public void AgainstInvalidUserId_ShouldThrowAPCShopExceptionWithTheCorrectMessageWhenTheGivenValueIsNull()
		{
			var ex = Assert.Throws<TechStoreException>(() => this.guard.AgainstInvalidUserId<object?>(null, this.message));

			Assert.That(ex.Message, Is.EqualTo(this.message));
		}

		[Test]
		public void AgainstNullOrEmptyCollection_ShouldThrowException_WhenCollectionIsNull()
		{
			// Arrange
			IEnumerable<int>? collection = null;
			var expectedMessage = "Collection cannot be null or empty.";

			// Act & Assert
			var ex = Assert.Throws<TechStoreException>(() =>
				this.guard.AgainstNullOrEmptyCollection(collection, expectedMessage));

			Assert.That(ex.Message, Is.EqualTo(expectedMessage));
		}

		[Test]
		public void AgainstNullOrEmptyCollection_ShouldThrowException_WhenCollectionIsEmpty()
		{
			// Arrange
			IEnumerable<int> collection = new List<int>();
			var expectedMessage = "Collection cannot be null or empty.";

			// Act & Assert
			var ex = Assert.Throws<TechStoreException>(() =>
				this.guard.AgainstNullOrEmptyCollection(collection, expectedMessage));

			Assert.That(ex.Message, Is.EqualTo(expectedMessage));
		}

		[Test]
		public void AgainstNullOrEmptyCollection_ShouldNotThrow_WhenCollectionIsNotEmpty()
		{
			// Arrange
			IEnumerable<int> collection = new List<int> { 1, 2, 3 };

			// Act & Assert
			Assert.DoesNotThrow(() => this.guard.AgainstNullOrEmptyCollection(collection, "Collection cannot be null or empty."));
		}

		[Test]
		public void AgainstNullOrEmptyCollection_ShouldThrowAnArgumentExceptionWithTheCorrectMessageWhenTheGivenCollectionIsNull()
		{
			var ex = Assert.Throws<TechStoreException>(() => this.guard.AgainstInvalidUserId<object?>(null, this.message));

			Assert.That(ex.Message, Is.EqualTo(this.message));
		}

		[Test]
		public void AgainstNullOrEmptyCollection_ShouldThrowTechStoreException_WhenCollectionIsEmpty()
		{
			// Arrange
			IEnumerable<int> collection = new List<int>();
			var expectedMessage = "Collection cannot be null or empty.";

			// Act & Assert
			var ex = Assert.Throws<TechStoreException>(() =>
				this.guard.AgainstNullOrEmptyCollection(collection, expectedMessage));

			Assert.That(ex.Message, Is.EqualTo(expectedMessage));
		}


		[Test]
		public void AgainstNullOrEmptyCollection_ShouldThrowTechStoreExceptionWithTheCorrectMessageWhenTheGivenCollectionIsEmpty()
		{
			// Arrange
			IEnumerable<int> collection = new List<int>();
			var expectedMessage = "Collection cannot be null or empty.";

			// Act & Assert
			var ex = Assert.Throws<TechStoreException>(() =>
				this.guard.AgainstNullOrEmptyCollection(collection, expectedMessage));

			Assert.That(ex.Message, Is.EqualTo(expectedMessage));
		}


		[Test]
		public void AgainstProductThatIsNull_ShouldThrowAnArgumentExceptionWhenTheGivenValueIsNull()
		{
			Assert.Throws<ArgumentException>(() => this.guard.AgainstProductThatIsNull<object?>(null));
		}

		[Test]
		public void AgainstProductThatIsNull_ShouldThrowAnArgumentExceptionWithTheCorrectMessageWhenTheGivenValueIsNull()
		{
			var ex = Assert.Throws<ArgumentException>(() => this.guard.AgainstProductThatIsNull<object?>(null, this.message));

			Assert.That(ex.Message, Is.EqualTo(this.message));
		}

		[Test]
		public void AgainstProductThatIsDeleted_ShouldThrowAnArgumentExceptionWhenTheGivenBooleanIsTrue()
		{
			Assert.Throws<ArgumentException>(() => this.guard.AgainstProductThatIsDeleted(true));
		}

		[Test]
		public void AgainstProductThatIsDeleted_ShouldThrowAnArgumentExceptionWithTheCorrectMessageWhenTheGivenBooleanIsTrue()
		{
			var ex = Assert.Throws<ArgumentException>(() => this.guard.AgainstProductThatIsDeleted(true, this.message));

			Assert.That(ex.Message, Is.EqualTo(this.message));
		}

		[Test]
		public void AgainstProductThatIsOutOfStock_ShouldThrowAnArgumentExceptionWhenTheGivenValueIsEqualToZero()
		{
			Assert.Throws<ArgumentException>(() => this.guard.AgainstProductThatIsOutOfStock(0));
		}

		[Test]
		public void AgainstProductThatIsOutOfStock_ShouldThrowAnArgumentExceptionWithTheCorrectMessageWhenTheGivenValueIsEqualToZero()
		{
			var ex = Assert.Throws<ArgumentException>(() => this.guard.AgainstProductThatIsOutOfStock(0, this.message));

			Assert.That(ex.Message, Is.EqualTo(this.message));
		}

		[Test]
		public void AgainstNotExistingValue_ShouldThrowAnArgumentExceptionWhenTheGivenValueIsNull()
		{
			Assert.Throws<ArgumentException>(() => this.guard.AgainstNotExistingValue<object?>(null));
		}

		[Test]
		public void AgainstNotExistingValue_ShouldThrowAnArgumentExceptionWithTheCorrectMessageWhenTheGivenValueIsNull()
		{
			var ex = Assert.Throws<ArgumentException>(() => this.guard.AgainstNotExistingValue<object?>(null, this.message));

			Assert.That(ex.Message, Is.EqualTo(this.message));
		}
	}
}
