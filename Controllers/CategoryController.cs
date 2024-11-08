using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(BlogDbContext context) : ControllerBase
    {
        private readonly BlogDbContext _context = context; // 数据上下文，用于与数据库交互

        // GET api/category
        // 获取所有分类的列表
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            // 从数据库中异步获取所有分类并返回
            return await _context.Categories.ToListAsync();
        }

        // POST api/category
        // 创建一个新的分类
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            // 将新分类添加到上下文
            _context.Categories.Add(category);

            // 保存更改到数据库
            await _context.SaveChangesAsync();

            // 返回创建的分类及其 URI
            return CreatedAtAction(nameof(GetCategories), new { id = category.Id }, category);
        }
        // 删除一个分类
        [HttpDelete("{id}")]
        public ActionResult DeleteCategory(int id) 
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return NoContent();
        }
    }
}