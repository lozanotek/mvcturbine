namespace MvcTurbine.Mv2.Tests.Areas {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Mvc;
	using ComponentModel;
	using Moq;
	using Mvc2;
	using NUnit.Framework;

	[TestFixture]
	public class RegistrationTest {

		[Test]
		public void Registration_Is_Added_To_List() {
			AreaBlade blade = new AreaBlade();

			var regList = new AutoRegistrationList();
			blade.AddRegistrations(regList);

			Assert.IsNotNull(regList);
			Assert.IsNotEmpty(regList.ToList());
		}

		[Test]
		[ExpectedException(typeof(NullReferenceException))]
		public void Registration_Fails_If_List_Is_Null() {
			AreaBlade blade = new AreaBlade();

			blade.AddRegistrations(null);
		}

		[Test]
		public void GetRegisteredAreas_Returns_Null_With_Null_ServiceLocator() {
			AreaBlade blade = new AreaBlade();

			var list = blade.GetRegisteredAreas(null);
			Assert.IsNull(list);
		}

		[Test]
		public void GetRegisteredAreas_Returns_Null_With_ServiceLocator() {
			AreaBlade blade = new AreaBlade();

			var mock = new Mock<IServiceLocator>();

			mock.Setup(locator =>
				locator.ResolveServices<AreaRegistration>())
					.Returns(() => null);

			var list = blade.GetRegisteredAreas(mock.Object);
			Assert.IsNull(list);
		}

		[Test]
		public void GetRegisteredAreas_Returns_EmptyList_With_ServiceLocator() {
			AreaBlade blade = new AreaBlade();

			var mock = new Mock<IServiceLocator>();

			mock.Setup(locator =>
				locator.ResolveServices<AreaRegistration>())
					.Returns(() => new List<AreaRegistration>());

			var list = blade.GetRegisteredAreas(mock.Object);

			Assert.IsNotNull(list);
			Assert.IsEmpty(list.ToArray());
		}

		[Test]
		public void GetRegisteredAreas_Returns_FullList_With_ServiceLocator() {
			AreaBlade blade = new AreaBlade();

			var mock = new Mock<IServiceLocator>();
			var areaMock = new Mock<AreaRegistration>().Object;

			mock.Setup(locator => locator.ResolveServices<AreaRegistration>())
				.Returns(() => new List<AreaRegistration> {areaMock});

			var list = blade.GetRegisteredAreas(mock.Object);

			Assert.IsNotNull(list);
			Assert.AreEqual(list.Count, 1);
			Assert.AreEqual(list[0], areaMock);
		}

	}
}
