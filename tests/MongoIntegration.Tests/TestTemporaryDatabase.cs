using System;
using MongoDB.Bson;
using MongoDB.Driver;
using Xunit;

namespace MongoIntegration.Tests
{
    public class TestTemporaryDatabase
    {
        [Fact]
        public void DatabasesAreCreatedWithARandomisedName()
        {
            var client = new MongoClient(new MongoUrl("mongodb://localhost"));
            using (var m = new TemporaryDatabase(client, "TestTemporaryDatabase"))
            {
                Assert.True(m.DatabaseName.StartsWith("TestTemporaryDatabase_"));
                Assert.True(m.DatabaseName.Length > "TestTemporaryDatabase_".Length);
            }
        }

        [Fact]
        public void ItIsPossibleToCreateCollectionsWithinATemporaryDatabase()
        {
            var client = new MongoClient(new MongoUrl("mongodb://localhost"));
            using (var m = new TemporaryDatabase(client, "TestTemporaryDatabase"))
            {
                m.CreateCollection("TestCollection");
                var collection = m.GetCollection<TestDocument>("TestCollection");
                collection.InsertOne(new TestDocument());
                Assert.Equal(1, collection.Count(x => true));
            }
        }

        [Fact]
        public void DatabasesAreDestroyedAfterUse()
        {
            var client = new MongoClient(new MongoUrl("mongodb://localhost"));
            string name = string.Empty;
            using (var m = new TemporaryDatabase(client, "TestTemporaryDatabase"))
            {
                m.CreateCollection("TestCollection");
                var collection = m.GetCollection<TestDocument>("TestCollection");
                collection.InsertOne(new TestDocument());
                name = m.DatabaseName;
            }
            Assert.NotEqual(string.Empty, name);
            Assert.False(client.GetDatabase(name).ListCollections().Any());
        }
    }

    public class TestDocument
    {
        public TestDocument()
        {
            this.Id = ObjectId.GenerateNewId();
        }
        public ObjectId Id { get; set; }
    }
}
