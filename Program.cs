using System;
using EFTutorial.Models;
using System.Linq;

namespace EFTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            var choice = "";

            do
            {
                Console.WriteLine();
                Console.WriteLine("enter your selection: ");
                Console.WriteLine("1. display all blogs");
                Console.WriteLine("2. add blog");
                Console.WriteLine("3. display posts");
                Console.WriteLine("4. create post");
                Console.WriteLine("enter 'q' to quit");

                choice = Console.ReadLine();

                switch (choice)
                {
                    //1. display blogs
                    case "1":
                        using (var db = new BlogContext())
                        {
                            if (db.Blogs.Any())
                            {
                                Console.WriteLine("list of blogs");
                                Console.WriteLine("--------------");
                                foreach (var b in db.Blogs)
                                {
                                    Console.WriteLine($"{b.BlogId}: {b.Name}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("no blogs in list");
                            }
                        }
                        break;

                    //2. add blog
                    case "2":
                        Console.WriteLine("enter blog name: ");
                        var blogName = Console.ReadLine();

                        // Create new Blog
                        var blog = new Blog
                        {
                            Name = blogName
                        };

                        // Save blog object to database
                        using (var db = new BlogContext()) 
                        {
                            db.Add(blog);
                            db.SaveChanges();
                        }
                        break; 

                    //3. display posts
                    case "3":
                        using (var db = new BlogContext())
                        {
                            if (db.Blogs.Any())
                            {
                                Console.WriteLine("enter blog id to view posts: ");
                                foreach (var b in db.Blogs)
                                {
                                    Console.WriteLine($"{b.BlogId}. {b.Name}");
                                }

                                var chosenId = int.Parse(Console.ReadLine());
                                var chosenBlog = db.Blogs.Where(b => b.BlogId == chosenId).FirstOrDefault();

                                if (chosenBlog != null)
                                {
                                    Console.WriteLine($"posts from {chosenBlog.Name}");
                                    Console.WriteLine($"number of posts: {chosenBlog.Posts.Count}");
                                    Console.WriteLine("------------------------");

                                    foreach (var p in chosenBlog.Posts) {
                                        Console.WriteLine($"\tPost {p.PostId}: {p.Title}\n{p.Content}");
                                    }
                                } else
                                {
                                    Console.WriteLine("please enter a valid blog id");
                                }
                            }
                            else
                            {
                                Console.WriteLine("no blogs in list");
                            }
                        }
                        break;

                    //4. add post
                    case "4":
                        using (var db = new BlogContext())
                        {
                            if (db.Blogs.Any())
                            {
                                Console.WriteLine("enter blog id to create a post: ");
                                foreach (var b in db.Blogs)
                                {
                                    Console.WriteLine($"{b.BlogId}. {b.Name}");
                                }
                                int chosenId = int.Parse(Console.ReadLine());
                                var chosenBlog = db.Blogs.Where(b => b.BlogId == chosenId).FirstOrDefault();

                                if (chosenBlog != null)
                                {
                                    Console.WriteLine("enter post title: ");
                                    var postTitle = Console.ReadLine();
                                    Console.WriteLine("enter post content: ");
                                    var postContent = Console.ReadLine();

                                    var post = new Post
                                    {
                                        Title = postTitle,
                                        Content = postContent,
                                        BlogId = chosenBlog.BlogId
                                    };
                                    
                                    db.Posts.Add(post);
                                    db.SaveChanges();
                                } else
                                {
                                    Console.WriteLine("please enter a valid blog id");
                                }
                            } else
                            {
                                Console.WriteLine("no blogs in list");
                            }
                        }
                        break;
                    case "q":
                        continue;
                    default:
                        Console.WriteLine("please enter a valid option");
                        break;
                }
            } while (choice != "q");
        }
    }
}
