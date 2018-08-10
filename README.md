# SafeResult

Simple Wrapper for avoiding exception handling and validation/guard logic in your ASP.NET Core controllers.

```csharp
using SamCo.AspNetCore.SafeResult;

public class PostsController : Controller
{
    private Result<Post> GetPost(int id)
    {
        try
        {
            Post post = Some.Legacy.Api.GetPost(id);
            if (post == null)
                return new NotFoundResult(); // Implicit cast
            return post; // also implicit cast
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

        return Ok(getPost.Value);
    }

    [HttpDelete("/post/{id}")]
    public IActionResult DeletePost(int id)
    {
        Result<User> getUser = _userService.GetCurrentUser();
        if (getUser.Errored) // Unauthorized or challenge
            return getUSer.ErrorResult;

        Result<Post> getPost = GetPost(id);
        if (getPost.Errored) // NotFound or InternalServerError
            return getPost.ErrorResult;

        // "void" methods can use non-generic Result
        Result deletePost = _newPostService.Delete(getPost.Value, getUser.Value);

        if (deletePost.Errored) // Forbid (user trying to delete post they don't own)
            return deletePost.ErrorResult;

        return NoContent(); // Finish
    }
}
```
