## MongoIntegrationContext

A simple library to create temporary databases for Integration Testing in MongoDB.

```c#
var client = new MongoClient(new MongoUrl("mongodb://localhost"));
// A random suffix is added to the database name.
using (var m = new TemporaryDatabase(client, "TestTemporaryDatabase"))
{
    // Use it as normal to test MongoDB queries.
    m.CreateCollection("TestCollection");
    var collection = m.GetCollection<TestDocument>("TestCollection");
    collection.InsertOne(new TestDocument());
    // When `m` is disposed of, the database is dropped.
}
```