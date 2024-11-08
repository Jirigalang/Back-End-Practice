using Microsoft.EntityFrameworkCore;

namespace BackEndPractice
{
    /// <summary>
    /// 博客文章结构
    /// </summary>
    public class Blog
    {
        public int Id { get; set; } // 主键
        public string? Title { get; set; } // 文章标题
        public string? CoverImage { get; set; } // 封面图像的路径或 URL
        public string? Content { get; set; } // 文章内容
        public DateTime CreatedAt { get; set; } // 创建时间
        public DateTime UpdatedAt { get; set; } // 更新时间
        public int CategoryId { get; set; } // 外键，指向分区
        public Category? Category { get; set; } // 导航属性
    }
    /// <summary>
    /// 用于返回博客文章的 DTO
    /// </summary>
    public class BlogDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? CoverImage { get; set; } // 封面图像的路径或 URL
        public DateTime CreatedAt { get; set; } // 创建时间
        public DateTime UpdatedAt { get; set; } // 更新时间
        public int CategoryId { get; set; } // 外键，指向分区
        public Category? Category { get; set; } // 导航属性
    }
    /// <summary>
    /// 为文章分区
    /// </summary>
    public class Category
    {  
        public int Id { get; set; } // 主键
        public string? Name { get; set; } // 分区名称
        public string? CoverImage { get; set; }// 分区封面图像的路径或 URL 
    }
    /// <summary>
    /// BlogDbContext 继承自 DbContext，代表与数据库的会话
    /// </summary>
    public class BlogDbContext(DbContextOptions<BlogDbContext> options) : DbContext(options)
    {
        // DbSet 属性用于定义与 BlogPost 实体的数据库表的映射
        public DbSet<Blog> BlogPosts { get; set; } // 博客文章集合

        // DbSet 属性用于定义与 Category 实体的数据库表的映射
        public DbSet<Category> Categories { get; set; } // 分区集合

        // 可以重写 OnModelCreating 方法以进一步配置模型
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 可在此处配置模型的关系和属性
            // 例如，设置 Blog 和 Category 之间的关系
            modelBuilder.Entity<Blog>()
                .HasOne(b => b.Category) // 每个 Blog 只有一个 Category
                .WithMany() // 每个 Category 可以有多个 BlogPost
                .HasForeignKey(b => b.CategoryId); // 设置外键
        }
    }
}