# Result

Simple Error Handling for ASP.NET Core!

```csharp
using SamCo.AspNetCore.SafeResult;

public Result<Post> GetPost(int id)
{
    try
    {
        var post = Some.Legacy.Api.GetPost(id);
        if (post == null)
            return new NotFoundResult();
        return post;
    }
    catch
    {
        // Log or whatever you want
        return StatusCodeResult(500);
    }
}

[HttpGet("/post/{id}")]
public IActionResult GetPost(int id)
{
    Result<Post> getPost = GetPost(id);
    if (getPost.Errored)
        return getPost.ErrorResult;

    return getPost.Value;
}
```