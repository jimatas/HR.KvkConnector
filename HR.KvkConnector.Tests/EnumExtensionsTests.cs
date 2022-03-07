using HR.KvkConnector.Infrastructure;
using HR.KvkConnector.Model;
using HR.KvkConnector.Model.Zoeken;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HR.KvkConnector.Tests
{
    [TestClass]
    public class EnumExtensionsTests
    {
        [TestMethod]
        public void GetStringValue_ByDefault_ReturnsStringValue()
        {
            // Arrange
            var enumMember = Adrestype.Bezoekadres;

            // Act
            var stringValue = enumMember.GetStringValue();

            // Assert
            Assert.AreEqual("bezoekadres", stringValue);
        }

        [DataTestMethod]
        [DataRow(Vestigingstype.Rechtspersoon | Vestigingstype.Hoofdvestiging, "hoofdvestiging, rechtspersoon")]
        [DataRow(Vestigingstype.Hoofdvestiging | Vestigingstype.Nevenvestiging, "hoofdvestiging, nevenvestiging")]
        [DataRow(Vestigingstype.Rechtspersoon | Vestigingstype.Hoofdvestiging | Vestigingstype.Nevenvestiging, "hoofdvestiging, nevenvestiging, rechtspersoon")]
        public void GetStringValue_GivenFlagsEnum_ReturnsExpectedStringValue(Vestigingstype enumMember, string expectedStringValue)
        {
            // Arrange

            // Act
            var stringValue = enumMember.GetStringValue();

            // Assert
            Assert.AreEqual(expectedStringValue, stringValue);
        }

        [TestMethod]
        public void GetStringValue_GivenDuplicateFlagsEnum_ReturnsNonduplicatedStringValue()
        {
            // Arrange
            var enumMember = Vestigingstype.Hoofdvestiging | Vestigingstype.Hoofdvestiging;

            // Act
            var stringValue = enumMember.GetStringValue();

            // Assert
            Assert.AreEqual("hoofdvestiging", stringValue);
        }

        [TestMethod]
        public void GetStringValue_GivenEnumWithoutEnumMemberAttribute_ReturnsToStringResult()
        {
            // Arrange
            var enumMember = JaNeeIndicatie.Ja;

            // Act
            var stringValue = enumMember.GetStringValue();

            // Assert
            Assert.AreEqual("Ja", stringValue);
        }
    }
}
