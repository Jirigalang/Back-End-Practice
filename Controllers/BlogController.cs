using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController(BlogDbContext context) : ControllerBase
    {
        private readonly BlogDbContext _context = context; // 数据上下文，用于与数据库交互

        // GET api/blog/category/{categoryId}
        // 根据分类 ID 获取博客文章
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<BlogDto>>> GetBlogPostsByCategory(int categoryId)
        {
            // 从数据库中异步获取指定分类 ID 的所有博客文章
            return await _context.BlogPosts
                                           .Select(blog => new BlogDto
                                           {
                                               Id = blog.Id,
                                               Title = blog.Title,
                                               CoverImage = blog.CoverImage,
                                               CreatedAt = blog.CreatedAt,
                                               UpdatedAt = blog.UpdatedAt,
                                               CategoryId = blog.CategoryId,
                                               Category = blog.Category
                                           })
                .Where(p => p.CategoryId == categoryId) // 按照分类 ID 过滤
                .ToListAsync(); // 异步转换为列表
        }

        // 根据 ID 获取单个博客文章
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlogPostById(int id)
        {
            // 从数据库中查找具有指定 ID 的博客文章
            var blog = await _context.BlogPosts
                                     .Where(b => b.Id == id)
                                     .Select(b => new Blog
                                     {
                                         Id = b.Id,
                                         Title = b.Title,
                                         CoverImage = b.CoverImage,
                                         CreatedAt = b.CreatedAt,
                                         UpdatedAt = b.UpdatedAt,
                                         CategoryId = b.CategoryId,
                                         Category = b.Category,
                                         Content = b.Content // 包含文章内容
                                     })
                                     .FirstOrDefaultAsync();

            if (blog == null)
            {
                return NotFound(); // 返回 404 状态，表示未找到
            }

            return Ok(blog); // 返回 200 状态和博客文章的详细信息
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogDto>>> GetAllBlogPosts()
        {
            // 从数据库中异步获取所有博客文章，选择所有属性但不包含 Content
            var blogPosts = await _context.BlogPosts
                                           .Select(blog => new BlogDto
                                           {
                                               Id = blog.Id,
                                               Title = blog.Title,
                                               CoverImage = blog.CoverImage,
                                               CreatedAt = blog.CreatedAt,
                                               UpdatedAt = blog.UpdatedAt,
                                               CategoryId = blog.CategoryId,
                                               Category = blog.Category // 如果需要包含类别信息
                                           })
                                           .ToListAsync(); // 异步转换为列表
            return Ok(blogPosts); // 返回 HTTP 200 状态和博客文章列表
        }


        // 创建一个新的博客文章
        [HttpPost]
        public async Task<ActionResult<Blog>> CreateBlogPost(Blog blog)
        {
            // 将新博客文章添加到上下文
            _context.BlogPosts.Add(blog);

            // 保存更改到数据库
            await _context.SaveChangesAsync();

            // 返回创建的博客文章及其 URI
            return CreatedAtAction(nameof(GetBlogPostsByCategory), new { categoryId = blog.CategoryId, id = blog.Id }, blog);
        }

        //删除一个博客文章
        [HttpDelete("{id}")]
        public ActionResult DeleteBlogPost(int id) 
        {
            var blog = _context.BlogPosts.Find(id);
            if (blog == null)
            {
                return NotFound();
            }
            _context.BlogPosts.Remove(blog);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
