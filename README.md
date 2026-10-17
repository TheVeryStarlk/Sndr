# Sndr

A robust & AOT-friendly thin wrapper around [SNDR](https://www.sndr.sh/)'s API. \
Still a work in progress & needs further testing. The important parts of the API are done.

## Usage

### Dependency injection

The library can be easily used using DI or without it.

```cs
var client = new SndrClient(Key);
```

Or using DI.

```cs
var services = new ServiceCollection();

services.AddSndrClient(options => options.Key = Key);

var client = services
    .BuildServiceProvider()
    .GetRequiredService<SndrClient>();
```

### Configuration

You can configure the resilience handler by options.

```cs
services.AddSndrClient(options =>
{
    options.Key = Key;
    options.MaximumAttempts = 10;
    options.MaximumDelay = TimeSpan.FromMilliseconds(100);
});
```

### Sending an email

This is just a single example of sending an email.

```cs
var client = new SndrClient(Key);

var request = new SendEmailRequest
{
    From = "hello@world.me",
    To = ["you@example.us"],
    Subject = "Hello, world!",
    Html = "<p>Hey!</p>"
};

var response = await client.SendEmailAsync(request);
```

### Receiving webhooks

Verify the `X-Sndr-Signature` header against the **raw** request body before parsing it.

```cs
app.MapPost("/webhooks/sndr", async (HttpRequest request) =>
{
    using var stream = new MemoryStream();

    await request.Body.CopyToAsync(stream);

    var body = stream.ToArray();

    if (!SndrWebhook.Verify(body, request.Headers[SndrWebhook.SignatureHeader], secret))
    {
        return Results.BadRequest();
    }

    switch (SndrWebhook.Deserialize(body))
    {
        // ...
    }

    return Results.Ok();
});
```