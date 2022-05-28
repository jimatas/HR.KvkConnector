using HR.KvkConnector.Infrastructure;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Runtime.Serialization;

namespace HR.KvkConnector.Tests
{
    [TestClass]
    public class JsonSerializerTests
    {
        [TestMethod]
        public void Serialize_GivenNullObject_ReturnsEmptyString()
        {
            // Arrange
            object objectGraph = null;

            // Act
            var jsonString = JsonSerializer.Serialize(objectGraph);

            // Assert
            Assert.AreEqual(string.Empty, jsonString);
        }

        [TestMethod]
        public void Serialize_GivenNonNullObject_ReturnsJsonString()
        {
            // Arrange
            var objectGraph = new Model.Handelsnaam();

            // Act
            var jsonString = JsonSerializer.Serialize(objectGraph);

            // Assert
            Assert.AreEqual("{\"naam\":null,\"volgorde\":null}", jsonString);
        }

        [TestMethod]
        public void Deserialize_GivenNullString_ReturnsNullObject()
        {
            // Arrange
            string jsonString = null;

            // Act
            var objectGraph = JsonSerializer.Deserialize<Model.Handelsnaam>(jsonString);

            // Assert
            Assert.AreEqual(null, objectGraph);
        }

        [TestMethod]
        public void Deserialize_GivenEmptyString_ReturnsNullObject()
        {
            // Arrange
            var jsonString = string.Empty;

            // Act
            var objectGraph = JsonSerializer.Deserialize<Model.Handelsnaam>(jsonString);

            // Assert
            Assert.AreEqual(null, objectGraph);
        }

        [DataTestMethod]
        [DataRow(" ")]
        [DataRow("x")]
        [DataRow(" { ] ")]
        public void Deserialize_GivenInvalidJsonString_ThrowsException(string jsonString)
        {
            // Arrange

            // Act
            void action() => JsonSerializer.Deserialize<Model.Handelsnaam>(jsonString);

            // Assert
            Assert.ThrowsException<SerializationException>(action);
        }

        [TestMethod]
        public void Deserialize_WithValidDateProvidedForDatumAanvangProperty_SetsPropertyToCorrectValue()
        {
            var jsonString = @"{""vestigingsnummer"":""000019061714"",""kvkNummer"":""04005136"",""rsin"":""002932064"",""indNonMailing"":""Nee"",""materieleRegistratie"":{""datumAanvang"":""19290101""},""eersteHandelsnaam"":""Actief B.V."",""indHoofdvestiging"":""Ja"",""indCommercieleVestiging"":""Ja"",""voltijdWerkzamePersonen"":0,""totaalWerkzamePersonen"":0,""deeltijdWerkzamePersonen"":0,""handelsnamen"":[{""naam"":""Actief B.V."",""volgorde"":0}],""adressen"":[{""type"":""bezoekadres"",""indAfgeschermd"":""Nee"",""volledigAdres"":""Industrieweg 9 7944HT Meppel"",""straatnaam"":""Industrieweg"",""huisnummer"":9,""postcode"":""7944HT"",""plaats"":""Meppel"",""land"":""Nederland""}],""websites"":[""www.noordhuis.nl""],""sbiActiviteiten"":[{""sbiCode"":""6810"",""sbiOmschrijving"":""Handel in eigen onroerend goed"",""indHoofdactiviteit"":""Ja""}],""links"":[{""rel"":""self"",""href"":""https://api.kvk.nl/api/v1/vestigingsprofielen/000019061714""},{""rel"":""basisprofiel"",""href"":""https://api.kvk.nl/api/v1/basisprofielen/04005136""}]}";

            var objectGraph = JsonSerializer.Deserialize<Model.Vestiging>(jsonString);

            Assert.AreEqual(new DateTime(year: 1929, month: 1, day: 1), objectGraph.MaterieleRegistratie.DatumAanvang);
        }

        [TestMethod]
        public void Deserialize_WithInvalidDateProvidedForDatumAanvangProperty_SetsPropertyToDateTimeMinValue()
        {
            var jsonString = @"{""vestigingsnummer"":""000019061714"",""kvkNummer"":""04005136"",""rsin"":""002932064"",""indNonMailing"":""Nee"",""materieleRegistratie"":{""datumAanvang"":""19290000""},""eersteHandelsnaam"":""Actief B.V."",""indHoofdvestiging"":""Ja"",""indCommercieleVestiging"":""Ja"",""voltijdWerkzamePersonen"":0,""totaalWerkzamePersonen"":0,""deeltijdWerkzamePersonen"":0,""handelsnamen"":[{""naam"":""Actief B.V."",""volgorde"":0}],""adressen"":[{""type"":""bezoekadres"",""indAfgeschermd"":""Nee"",""volledigAdres"":""Industrieweg 9 7944HT Meppel"",""straatnaam"":""Industrieweg"",""huisnummer"":9,""postcode"":""7944HT"",""plaats"":""Meppel"",""land"":""Nederland""}],""websites"":[""www.noordhuis.nl""],""sbiActiviteiten"":[{""sbiCode"":""6810"",""sbiOmschrijving"":""Handel in eigen onroerend goed"",""indHoofdactiviteit"":""Ja""}],""links"":[{""rel"":""self"",""href"":""https://api.kvk.nl/api/v1/vestigingsprofielen/000019061714""},{""rel"":""basisprofiel"",""href"":""https://api.kvk.nl/api/v1/basisprofielen/04005136""}]}";

            var objectGraph = JsonSerializer.Deserialize<Model.Vestiging>(jsonString);

            Assert.AreEqual(DateTime.MinValue, objectGraph.MaterieleRegistratie.DatumAanvang);
        }

        [TestMethod]
        public void Deserialize_WithNullValueProvidedForDatumAanvangProperty_SetsPropertyToNull()
        {
            var jsonString = @"{""vestigingsnummer"":""000019061714"",""kvkNummer"":""04005136"",""rsin"":""002932064"",""indNonMailing"":""Nee"",""materieleRegistratie"":{""datumAanvang"":null},""eersteHandelsnaam"":""Actief B.V."",""indHoofdvestiging"":""Ja"",""indCommercieleVestiging"":""Ja"",""voltijdWerkzamePersonen"":0,""totaalWerkzamePersonen"":0,""deeltijdWerkzamePersonen"":0,""handelsnamen"":[{""naam"":""Actief B.V."",""volgorde"":0}],""adressen"":[{""type"":""bezoekadres"",""indAfgeschermd"":""Nee"",""volledigAdres"":""Industrieweg 9 7944HT Meppel"",""straatnaam"":""Industrieweg"",""huisnummer"":9,""postcode"":""7944HT"",""plaats"":""Meppel"",""land"":""Nederland""}],""websites"":[""www.noordhuis.nl""],""sbiActiviteiten"":[{""sbiCode"":""6810"",""sbiOmschrijving"":""Handel in eigen onroerend goed"",""indHoofdactiviteit"":""Ja""}],""links"":[{""rel"":""self"",""href"":""https://api.kvk.nl/api/v1/vestigingsprofielen/000019061714""},{""rel"":""basisprofiel"",""href"":""https://api.kvk.nl/api/v1/basisprofielen/04005136""}]}";

            var objectGraph = JsonSerializer.Deserialize<Model.Vestiging>(jsonString);

            Assert.IsNull(objectGraph.MaterieleRegistratie.DatumAanvang);
        }
    }
}
