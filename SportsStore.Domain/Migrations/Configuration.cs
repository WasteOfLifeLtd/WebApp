namespace SportsStore.Domain.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.IO;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SportsStore.Domain.Entities;
    using SportsStore.Domain.Entities.IdentityEntities;
    

    internal sealed class Configuration : DbMigrationsConfiguration<SportsStore.Domain.Concrete.AppIdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SportsStore.Domain.Concrete.AppIdentityDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            AppUserManager userMgr = new AppUserManager(new UserStore<AppUser>(context));
            AppRoleManager roleMgr = new AppRoleManager(new RoleStore<AppRole>(context));

            // Initializing an administrator
            string roleName = "Administrators";
            string userName = "Admin";
            string firstName = "The";
            string lastName = "One";
            string password = "MySecret";
            string email = "serenewayfaringstranger@gmail.com";
            initializeUser();
           
            // Initializing a manager
            roleName = "Managers";
            userName = "Manager1";
            firstName = "Pablo";
            lastName = "Huarez";
            password = "MySecret";
            email = "whatever@mail.com";
            initializeUser();

            // Initializing a customer
            roleName = "Customers";
            userName = "Customer1";
            firstName = "Juan";
            lastName = "Lobos";
            password = "MySecret";
            email = "whateverwhatever@mail.com";
            initializeUser();

            void initializeUser()
            {
                if (!roleMgr.RoleExists(roleName))
                {
                    roleMgr.Create(new AppRole(roleName));
                }
                AppUser user = userMgr.FindByName(userName);
                if (user == null)
                {
                    userMgr.Create(new AppUser { UserName = userName, Email = email, FirstName = firstName, LastName = lastName },
                    password);
                    user = userMgr.FindByName(userName);
                }
                if (!userMgr.IsInRole(user.Id, roleName))
                {
                    userMgr.AddToRole(user.Id, roleName);
                }
                context.SaveChanges();
            }


            // inventory seed portion
            if (context.Books.Count() == 0 && context.Authors.Count() == 0 && context.Categories.Count() == 0) {
                //seeding books
                context.Books.AddOrUpdate(new Book
                {
                Title = "Beginning Xamarin Development for the Mac",
                Price = 24.99m,
                ISBN = "978-1-4842-3132-6",
                Description = @"Develop apps for the iPhone, iPad, and Apple wearables using Visual Studio for the Mac. Learn how to set up your development environment and emulators, and how to create adaptive user interfaces for various platforms. Expert Dawid Borycki guides you through the fundamentals of programming for Apple platforms (Model View Controller, Test Driven Development), navigation patterns, gesture handling, accessing user's location, and reading and consuming data from web services. After reading this book, you will be able to build native apps that look and feel like other apps built into iOS, watchOS, and tvOS, and have the skills that are in high demand in today’s market.If you are already programming C# apps for web or desktop, you will learn how to extend your skill set to Apple mobile, wearable, and smart TV platforms.",
                CoverImage = File.ReadAllBytes(@"C:\Users\dsds\Desktop\ASP.NET\SportsStore\SportsStore.WebUI\App_Data\Beginning Xamarin Development for the Mac.jpg"),
                ImageMimeType = "image/jpeg",
                Authors = new List<Author>(),
                Orders = new List<Order>()
            },
            new Book
            {
                Title = "The Business of Machine Learning",
                Price = 59.99m,
                ISBN = "978-1-4842-3543-0",
                Description = @"Successfully and proactively take charge of your machine learning strategy. Machine learning (ML) is permeating every sector and aspect of business, from evaluating the success of a massive online marketing campaign, to predicting insurance payouts, to crime scene analysis. This book shows how to interpret patterns and redundancies from massive amounts of existing data to help your business cut costs, operate more efficiently and effectively, and get to the next level. You will learn how to analyze, communicate, and launch a viable program that, when done correctly, will positively transform your business.The authors engage you to experience the business of machine learning through actual conversations that open with an exchange between a data scientist and and his counterpart in business, the technical decision maker.You will learn where to go when the conversation leads to an impasse and work step - by - step to methodically resolve the challenges.After reading this book, you will come away with the confidence to tackle a machine learning strategy customized for your team or business objectives.Revel in the vast capabilities of machine learning tools at your disposal and reach that ""a ha"" moment when you discover the profound and enduring impact machine learning can have on your business.",
                CoverImage = File.ReadAllBytes(@"C:\Users\dsds\Desktop\ASP.NET\SportsStore\SportsStore.WebUI\App_Data\The Business of Machine Learning.jpg"),
                ImageMimeType = "image/jpeg",
                Authors = new List<Author>(),
                Orders = new List<Order>()
            },
            new Book
            {
                Title = "C# 7 Quick Syntax Reference",
                Price = 19.99m,
                ISBN = "978-1-4842-3817-2",
                Description = @"This quick C# 7 guide is a condensed code and syntax reference to the C# programming language, updated with the latest features of C# 7.3 for .NET and Windows 10. It presents the essential C# 7 syntax in a well-organized format that can be used as a handy reference. In the C# 7 Quick Syntax Reference, you will find a concise reference to the C# language syntax: short, simple, and focused code examples; a well laid out table of contents; and a comprehensive index allowing easy review. You won’t find any technical jargon, bloated samples, drawn-out history lessons, or witty stories. What you will find is a language reference that is concise, to the point, and highly accessible. The book is packed with useful information and is a must-have for any C# programmer.",
                CoverImage = File.ReadAllBytes(@"C:\Users\dsds\Desktop\ASP.NET\SportsStore\SportsStore.WebUI\App_Data\C# 7 Quick Syntax Reference.jpg"),
                ImageMimeType = "image/jpeg",
                Authors = new List<Author>(),
                Orders = new List<Order>()
            },
            new Book
            {
                Title = "Yammer",
                Price = 24.99m,
                ISBN = "978-1-4842-3796-0",
                Description = @"Build a successful Yammer implementation, make your workplace social and collaborative, create a culture of sharing, form expert communities and generate innovative solutions. Besides, this book will help to enhance your collaboration your suppliers, partners, and clients.
                    The author starts by giving an introduction to social collaborations and successful implementations of Yammer. Along the way, he explains the art of community management in Yammer using his hands-on experience of building communities. He then explains methods to keep a Yammer network engaged followed by a description of running a campaign on Yammer.
                    The second part of Yammer begins with ways you can engage entire organizations, including executives, on Yammer along with methods to measure the success of a Yammer network. You’ll see how to get to grips with integrating Yammer with an existing platform and how to collaborate with customers, suppliers, and partners using Yammer. Finally, you’ll learn various innovative techniques of communication using Yammer and explore the author’s vision of the next-generation Yammer platform
                    After reading this book you will understand how to make successful Yammer implementations, engage communities on Yammer, and  accomplish business goals using Yammer.",
                CoverImage = File.ReadAllBytes(@"C:\Users\dsds\Desktop\ASP.NET\SportsStore\SportsStore.WebUI\App_Data\Yammer.jpg"),
                ImageMimeType = "image/jpeg",
                Authors = new List<Author>(),
                Orders = new List<Order>()
            },
            new Book
            {
                Title = "Mastering Microsoft Teams",
                Price = 24.99m,
                ISBN = "978-1-4842-3670-3",
                Description = @"Do you need to learn how to use Microsoft Teams? Are you questioning how to drive user adoption, govern content, and manage access for your Teams deployment? Either way, Mastering Microsoft Teams is your one-stop-shop to learning everything you need to know to find success with Microsoft Teams.
                    Microsoft’s new chat-based collaboration software has many rich features that enable teams to be more efficient, and save valuable time and resources. However, as with all software, there is a learning curve and pitfalls that should be avoided.
                    Begin by learning the core components and use cases for Teams. From there the authors guide you through ideas to create governance and adoption plans that make sense for your organization or customer. Wrap up with an understanding of features and services in progress, and a road map to the future of the product.",
                CoverImage = File.ReadAllBytes(@"C:\Users\dsds\Desktop\ASP.NET\SportsStore\SportsStore.WebUI\App_Data\Mastering Microsoft Teams.jpg"),
                ImageMimeType = "image/jpeg",
                Authors = new List<Author>(),
                Orders = new List<Order>()
            },
            new Book
            {
                Title = "Cosmos DB for MongoDB Developers",
                Price = 24.99m,
                ISBN = "978-1-4842-3682-6",
                Description = @"Learn Azure Cosmos DB and its MongoDB API with hands-on samples and advanced features such as the multi-homing API, geo-replication, custom indexing, TTL, request units (RU), consistency levels, partitioning, and much more. Each chapter explains Azure Cosmos DB’s features and functionalities by comparing it to MongoDB with coding samples. 
                    Cosmos DB for MongoDB Developers starts with an overview of NoSQL and Azure Cosmos DB and moves on to demonstrate the difference between geo-replication of Azure Cosmos DB compared to MongoDB. Along the way you’ll cover subjects including indexing, partitioning, consistency, and sizing, all of which will help you understand the concepts of read units and how this calculation is derived from an existing MongoDB’s usage. 
                    The next part of the book shows you the process and strategies for migrating to Azure Cosmos DB. You will learn the day-to-day scenarios of using Azure Cosmos DB, its sizing strategies, and optimizing techniques for the MongoDB API. This information will help you when planning to migrate from MongoDB or if you would like to compare MongoDB to the Azure Cosmos DB MongoDB API before considering the switch.",
                CoverImage = File.ReadAllBytes(@"C:\Users\dsds\Desktop\ASP.NET\SportsStore\SportsStore.WebUI\App_Data\Cosmos DB for MongoDB Developers.jpg"),
                ImageMimeType = "image/jpeg",
                Authors = new List<Author>(),
                Orders = new List<Order>()
            },
            new Book
            {
                Title = "Deep Learning with Azure",
                Price = 29.99m,
                ISBN = "978-1-4842-3679-6",
                Description = @"Get up-to-speed with Microsoft's AI Platform. Learn to innovate and accelerate with open and powerful tools and services that bring artificial intelligence to every data scientist and developer.
                    Artificial Intelligence (AI) is the new normal. Innovations in deep learning algorithms and hardware are happening at a rapid pace. It is no longer a question of should I build AI into my business, but more about where do I begin and how do I get started with AI?
                    Written by expert data scientists at Microsoft, Deep Learning with the Microsoft AI Platform helps you with the how-to of doing deep learning on Azure and leveraging deep learning to create innovative and intelligent solutions. Benefit from guidance on where to begin your AI adventure, and learn how the cloud provides you with all the tools, infrastructure, and services you need to do AI.",
                CoverImage = File.ReadAllBytes(@"C:\Users\dsds\Desktop\ASP.NET\SportsStore\SportsStore.WebUI\App_Data\Deep Learning with Azure.jpg"),
                ImageMimeType = "image/jpeg",
                Authors = new List<Author>(),
                Orders = new List<Order>()
            },
            new Book
            {
                Title = "IoT, AI, and Blockchain for .NET",
                Price = 34.99m,
                ISBN = "978-1-4842-3709-0",
                Description = @"Create applications using Industry 4.0. Discover how artificial intelligence (AI) and machine learning (ML) capabilities can be enhanced using the Internet of things (IoT) and secured using Blockchain, so your latest app can be not just smarter but also more connected and more secure than ever before. This book covers the latest easy-to-use APIs and services from Microsoft, including Azure IoT, Cognitive Services APIs, Blockchain as a Service (BaaS), and Machine Learning Studio.
                    As you work through the book, you’ll get hands-on experience building an example solution that uses all of these technologies—an IoT suite for a smart healthcare facility. Hosted on Azure and networked using Azure IoT, the solution includes centralized patient monitoring, using Cognitive Services APIs for face detection, recognition, and tracking. Blockchain is used to create trust-based security and inventory management. Machine learning is used to create predictive solutions to proactively improve quality of life. By the end of the book, you’ll be confident creating richer and smarter applications using these technologies.",
                CoverImage = File.ReadAllBytes(@"C:\Users\dsds\Desktop\ASP.NET\SportsStore\SportsStore.WebUI\App_Data\IoT, AI, and Blockchain for .NET.jpg"),
                ImageMimeType = "image/jpeg",
                Authors = new List<Author>(),
                Orders = new List<Order>()
            },
            new Book
            {
                Title = "Practical Microsoft Azure IaaS",
                Price = 29.99m,
                ISBN = "978-1-4842-3763-2",
                Description = @"Adopt Azure IaaS and migrate your on-premise infrastructure partially or fully to Azure. This book provides practical solutions by following Microsoft’s design and best practice guidelines for building highly available, scalable, and secure solution stacks using Microsoft Azure IaaS. 
                    The author starts by giving an overview of Azure IaaS and its components: you’ll see the new aspects of Azure Resource Manager, storage in IaaS, and Azure networking. As such, you’ll cover design considerations for migration and implementation of infrastructure services, giving you practical skills to apply to your own projects. 
                    The next part of the book takes you through the different components of Azure IaaS that need to be included in a resilient architecture and how to set up a highly available infrastructure in Azure. The author focuses on the tools available for Azure IaaS automated provisioning and the different performance monitoring and fine-tuning options available for the platform. Finally, you’ll gain practical skills in Azure security and implementing Azure architectures.
                    After reading Practical Microsoft Azure IaaS, you will have learned how to map the familiar on-premise architecture components to their cloud infrastructure counterparts. This book provides a focused and practical approach to designing solutions to be hosted in Azure IaaS.",
                CoverImage = File.ReadAllBytes(@"C:\Users\dsds\Desktop\ASP.NET\SportsStore\SportsStore.WebUI\App_Data\Practical Microsoft Azure IaaS.jpg"),
                ImageMimeType = "image/jpeg",
                Authors = new List<Author>(),
                Orders = new List<Order>()
            },
            new Book
            {
                Title = "Practical Bot Development",
                Price = 39.99m,
                ISBN = "978-1-4842-3540-9",
                Description = @"Explore the concept of bots and discover the motivation behind working with these new apps with messaging platforms. This book is an accessible resource teaching the basic concepts behind bot design and implementation. Each chapter builds on previous topics and, where appropriate, real working code is shown that implements the concepts. By just picking up a code editor, you can start creating smart, engaging, and useful bot experiences today.
                    Practical Bot Development will teach you how to create your own bots on platforms like Facebook Messenger and Slack, incorporate extension APIs, and apply AI and ML algorithms in the cloud. By the end of this book, you'll be equipped with the information to reach thousands of new users with the bots you create!
                    The book is a great resource for those looking to harness the benefits of building their own bots and leveraging the platform feasibility of them.",
                CoverImage = File.ReadAllBytes(@"C:\Users\dsds\Desktop\ASP.NET\SportsStore\SportsStore.WebUI\App_Data\Practical Bot Development.jpg"),
                ImageMimeType = "image/jpeg",
                Authors = new List<Author>(),
                Orders = new List<Order>()
            },
            new Book
            {
                Title = "Introducing Microsoft Teams",
                Price = 29.99m,
                ISBN = "978-1-4842-3567-6",
                Description = @"Gain industry best practices from planning to implementing Microsoft Teams and learn how to enable, configure, and integrate user provisioning, management, and monitoring. This book also covers troubleshooting Teams with step-by-step instructions and examples. Introducing Microsoft Teams gives you the comprehensive coverage you need to creatively utilize Microsoft Teams services.
                    The author starts by giving an introduction to Microsoft Teams and its architecture followed by optimizing the Teams experience where he describes how organizations can prepare for Teams and enhance existing services. He further shows you how to manage and control the Microsoft Teams experience along with its capabilities and enhancements. You’ll learn how to migrate from Skype for Business to Microsoft Teams with a step-by-step tutorial. Finally, you’ll get to grips with Teams troubleshooting and best practices.
                    This book has detailed coverage that helps you exploit every capability Microsoft Teams has to offer. It provides the answers you need and the insight that will make your journey from Skype for Business to Teams easier. ",
                CoverImage = File.ReadAllBytes(@"C:\Users\dsds\Desktop\ASP.NET\SportsStore\SportsStore.WebUI\App_Data\Introducing Microsoft Teams.jpg"),
                ImageMimeType = "image/jpeg",
                Authors = new List<Author>(),
                Orders = new List<Order>()
            },
            new Book
            {
                Title = "Beginning PHP and MySQL",
                Price = 39.99m,
                ISBN = "978-1-4302-6044-8",
                Description = @"Get started with PHP and MySQL programming: no experience necessary. This fifth edition of a classic best-seller includes detailed instructions for configuring the ultimate PHP 7 and MySQL development environment on all major platforms, complete coverage of the latest additions and improvements to the PHP language, coverage of the Composer dependency manager, and thorough introductions to MySQL’s most relied-upon features. Beginning PHP and MySQL: From Novice to Professional, Fifth Edition is a major update of W. Jason Gilmore's authoritative book on PHP and MySQL. 
                    You'll not only receive extensive introductions to the core features of PHP, MySQL, and related tools, but you'll also learn how to effectively integrate them in order to build robust data-driven applications. Authors Jason Gilmore and Frank Kromann draw upon more than 15 years of experience working with these technologies to pack this book with practical examples and insight into the real-world challenges faced by developers. Accordingly, you will repeatedly return to this book as both a valuable instructional tool and reference guide. ",
                CoverImage = File.ReadAllBytes(@"C:\Users\dsds\Desktop\ASP.NET\SportsStore\SportsStore.WebUI\App_Data\Beginning PHP and MySQL.jpg"),
                ImageMimeType = "image/jpeg",
                Authors = new List<Author>(),
                Orders = new List<Order>()
            },
            new Book
            {
                Title = "Beginning SVG",
                Price = 29.99m,
                ISBN = "978-1-4842-3760-1",
                Description = @"Develop SVG functionality for use within websites quickly and natively, using basic tools such as HTML and CSS. This book is a project-oriented guide to creating and manipulating scalable vector graphics in the browser for websites or online applications, using little more than a text editor or free software, and the power of JavaScript. 
                    You'll use a starting toolset to incorporate into your existing workflow, develop future projects, and reduce any dependency on graphics applications for simple projects. This book is an excellent resource for getting acquainted with creating and manipulating SVG content. 
                    We live in an age where speed and simplicity are of the essence. Beginning SVG provides a perfect alternative when creating web-based projects that challenges the norm and encourages you to expand your resources and not resort to what “everyone else uses” (such as Illustrator). You'll discover that there is indeed a different way to achieve the same result. Stop thinking you must always resort to using graphics packages; there is always another way!",
                CoverImage = File.ReadAllBytes(@"C:\Users\dsds\Desktop\ASP.NET\SportsStore\SportsStore.WebUI\App_Data\Beginning SVG.jpg"),
                ImageMimeType = "image/jpeg",
                Authors = new List<Author>(),
                Orders = new List<Order>()
            },
            new Book
            {
                Title = "Web Applications with Elm",
                Price = 19.99m,
                ISBN = "978-1-4842-2610-0",
                Description = @"Learn the basics of the Elm platform for web applications. This book covers the language as of version 0.18 and the most important libraries. 
                    After reading this book you will have an understanding what Elm can do for you. Also, you will be able to build on the example in the book to develop advanced web applications with Elm.",
                CoverImage = File.ReadAllBytes(@"C:\Users\dsds\Desktop\ASP.NET\SportsStore\SportsStore.WebUI\App_Data\Web Applications with Elm.jpg"),
                ImageMimeType = "image/jpeg",
                Authors = new List<Author>(),
                Orders = new List<Order>()
            },
            new Book
            {
                Title = "HTML5 and JavaScript Projects",
                Price = 34.99m,
                ISBN = "978-1-4842-3864-6",
                Description = @"Build on your basic knowledge of HTML5 and JavaScript to create substantial HTML5 applications. Through the many interesting projects you can create in this book, you'll develop HTML5 skills for future projects, and extend the core skills you may have learned with its companion book, The Essential Guide to HTML5.
                    HTML5 and JavaScript Projects is fully updated as a second edition and covers important programming techniques and HTML, CSS, and JavaScript features to help you build projects with images, animation, video, audio and line drawings. You'll learn how to build games, quizzes and other interactive projects; incorporate the use of the Google Maps API and localStorage; and address the challenges of Responsive Design and Accessibility.
                    Each project starts out with a description of the example's operation, often with full-color illustrations.  You'll then review the HTML5 and JavaScript concepts that relate to the project followed by a step-by-step explanation of the programming used. Tables are used to show the relationship of functions and provide comments for each line of code so that you can easily apply the techniques to your own HTML5 projects.​",
                CoverImage = File.ReadAllBytes(@"C:\Users\dsds\Desktop\ASP.NET\SportsStore\SportsStore.WebUI\App_Data\HTML5 and JavaScript Projects.jpg"),
                ImageMimeType = "image/jpeg",
                Authors = new List<Author>(),
                Orders = new List<Order>()
            },
            new Book
            {
                Title = "REST API Development with Node.js",
                Price = 29.99m,
                ISBN = "978-1-4842-3715-1",
                Description = @"Manage and understand the full capabilities of successful REST development. REST API development is a hot topic in the programming world, but not many resources exist for developers to really understand how you can leverage the advantages.
                    This completely updated second edition provides a brief background on REST and the tools it provides (well known and not so well known), then explains how there is more to REST than just JSON and URLs. You will learn about the maintained modules currently available in the npm community, including Express, Restify, Vatican, and Swagger. Finally you will code an example API from start to finish, using a subset of the tools covered.
                    The Node community is currently flooded with modules; some of them are published once and never updated again - cluttering the entire universe of packages. Pro REST API Development with Node.js shines light into that black hole of modules for the developers trying to create an API. Understand REST API development with Node.js using this book today.",
                CoverImage = File.ReadAllBytes(@"C:\Users\dsds\Desktop\ASP.NET\SportsStore\SportsStore.WebUI\App_Data\REST API Development with Node.js.jpg"),
                ImageMimeType = "image/jpeg",
                Authors = new List<Author>(),
                Orders = new List<Order>()
            },
            new Book
            {
                Title = "Scalability Patterns",
                Price = 19.99m,
                ISBN = "978-1-4842-1073-4",
                Description = @"In this book, the CEO of Cazton, Inc. and internationally-acclaimed speaker, Chander Dhall, demonstrates current website design scalability patterns and takes a pragmatic approach to explaining their pros and cons to show you how to select the appropriate pattern for your site. He then tests the patterns by deliberately forcing them to fail and exposing potential flaws before discussing how to design the optimal pattern to match your scale requirements. The author explains the use of polyglot programming and how to match the right patterns to your business needs. He also details several No-SQL patterns and explains the fundamentals of different paradigms of No-SQL by showing complementary strategies of using them along with relational databases to achieve the best results. He also teaches how to make the scalability pattern work with a real-world microservices pattern. 
                    With the proliferation of countless electronic devices and the ever growing number of Internet users, the scalability of websites has become an increasingly important challenge. Scalability, even though highly coveted, may not be so easy to achieve. Think that you can't attain responsiveness along with scalability? Chander Dhall will demonstrate that, in fact, they go hand in hand.",
                CoverImage = File.ReadAllBytes(@"C:\Users\dsds\Desktop\ASP.NET\SportsStore\SportsStore.WebUI\App_Data\Scalability Patterns.jpg"),
                ImageMimeType = "image/jpeg",
                Authors = new List<Author>(),
                Orders = new List<Order>()
            },
            new Book
            {
                Title = "SQL Server 2017 Query Performance Tuning",
                Price = 44.99m,
                ISBN = "978-1-4842-3888-2",
                Description = @"Identify and fix causes of poor performance. You will learn Query Store, adaptive execution plans, and automated tuning on the Microsoft Azure SQL Database platform. Anyone responsible for writing or creating T-SQL queries will find valuable the insight into bottlenecks, including how to recognize them and eliminate them.
                    This book covers the latest in performance optimization features and techniques and is current with SQL Server 2017. If your queries are not running fast enough and you’re tired of phone calls from frustrated users, then this book is the answer to your performance problems. 
                    SQL Server 2017 Query Performance Tuning is about more than quick tips and fixes. You’ll learn to be proactive in establishing performance baselines using tools such as Performance Monitor and Extended Events. You’ll recognize bottlenecks and defuse them before the phone rings. You’ll learn some quick solutions too, but emphasis is on designing for performance and getting it right. The goal is to head off trouble before it occurs.",
                CoverImage = File.ReadAllBytes(@"C:\Users\dsds\Desktop\ASP.NET\SportsStore\SportsStore.WebUI\App_Data\SQL Server 2017 Query Performance Tuning.jpg"),
                ImageMimeType = "image/jpeg",
                Authors = new List<Author>(),
                Orders = new List<Order>()
            },
            new Book
            {
                Title = "Introducing InnoDB Cluster",
                Price = 29.99m,
                ISBN = "978-1-4842-3885-1",
                Description = @"Set up, manage, and configure the new InnoDB Cluster feature in MySQL from Oracle. If you are growing your MySQL installation and want to explore making your servers highly available, this book provides what you need to know about high availability and the new tools that are available in MySQL 8.0.11 and later. 
                    Introducing InnoDB Cluster teaches you about the building blocks that make up InnoDB Cluster such as MySQL Group Replication for storing data redundantly, MySQL Router for the routing of inbound connections, and MySQL Shell for simplified setup and configuration, status reporting, and even automatic failover. You will understand how it all works together to ensure that your data are available even when your primary database server goes down. 
                    Features described in this book are available in the Community Edition of MySQL, beginning with the version 8.0.11 GA release, making this book relevant for any MySQL users in need of redundancy against failure. Tutorials in the book show how to configure a test environment and plan a production deployment. Examples are provided in the form of a walk-through of a typical MySQL high-availability setup.",
                CoverImage = File.ReadAllBytes(@"C:\Users\dsds\Desktop\ASP.NET\SportsStore\SportsStore.WebUI\App_Data\Introducing InnoDB Cluster.jpg"),
                ImageMimeType = "image/jpeg",
                Authors = new List<Author>(),
                Orders = new List<Order>()
            },
            new Book
            {
                Title = "Beginning Blockchain",
                Price = 24.99m,
                ISBN = "978-1-4842-3444-0",
                Description = @"Understand the nuts and bolts of Blockchain, its different flavors with simple use cases, and cryptographic fundamentals. You will also learn some design considerations that can help you build custom solutions.
                    Beginning Blockchain is a beginner’s guide to understanding the core concepts of Blockchain from a technical perspective. By learning the design constructs of different types of Blockchain, you will get a better understanding of building the best solution for specific use cases. The book covers the technical aspects of Blockchain technologies, cryptography, cryptocurrencies, and distributed consensus mechanisms. You will learn how these systems work and how to engineer them to design next-gen business solutions.",
                CoverImage = File.ReadAllBytes(@"C:\Users\dsds\Desktop\ASP.NET\SportsStore\SportsStore.WebUI\App_Data\Beginning Blockchain.jpg"),
                ImageMimeType = "image/jpeg",
                Authors = new List<Author>(),
                Orders = new List<Order>()
            },
            new Book
            {
                Title = "Introducing the MySQL 8 Document Store",
                Price = 34.99m,
                ISBN = "978-1-4842-2725-1",
                Description = @"Learn the new Document Store feature of MySQL 8 and build applications around a mix of the best features from SQL and NoSQL database paradigms. Don’t allow yourself to be forced into one paradigm or the other, but combine both approaches by using the Document Store. 
                    MySQL 8 was designed from the beginning to bridge the gap between NoSQL and SQL. Oracle recognizes that many solutions need the capabilities of both. More specifically, developers need to store objects as loose collections of schema-less documents, but those same developers also need the ability to run structured queries on their data. With MySQL 8, you can do both! Introducing the MySQL 8 Document Store presents new tools and features that make creating a hybrid database solution far easier than ever before. This book covers the vitally important MySQL Document Store, the new X Protocol for developing applications, and a new client shell called the MySQL Shell. Also covered are supporting technologies and concepts such as JSON, schema-less documents, and more. The book gives insight into how features work and how to apply them to get the most out of your MySQL experience.",
                CoverImage = File.ReadAllBytes(@"C:\Users\dsds\Desktop\ASP.NET\SportsStore\SportsStore.WebUI\App_Data\Introducing the MySQL 8 Document Store.jpg"),
                ImageMimeType = "image/jpeg",
                Authors = new List<Author>(),
                Orders = new List<Order>()
            });

            //seeding authors
            context.Authors.AddOrUpdate(new Author
            {
                FirstName = "Josh",
                LastName = "Holmes",
                Description = @"Josh Holmes is CTO of the Commercial Software Engineering Americas team at Microsoft. Prior to joining Microsoft, Josh consulted for a variety of clients ranging from large Fortune 500 firms to startups. Josh speaks and presents globally on the topics of IoT and machine learning. A tireless and passionate advocate for the tech community, Josh has founded and/or run numerous organizations, including the Great Lakes Area .NET Users Group and the Ann Arbor Computer Society. He was also on the forming committee for CodeMash. You can contact Josh through his blog."
            },
            new Author
            {
                FirstName = "Mike",
                LastName = "Lanzetta ",
                Description = @"Mike Lanzetta designs and implements machine learning solutions for Fortune 500 companies at Microsoft. He has been doing software development for more than 20 years, working at four-person startups to Amazon. His experience runs the gamut from electronic circuit design, travel optimization, and drug discovery to demand forecasting at Amazon and machine learning at Microsoft. Mike regularly presents and chairs at conferences nationally and internationally. He has an M.Sc. in CSE from UW and a B.Sc. in CE from UCSC. He is often found blogging or tweeting on the topic of machine learning."
            },
            new Author
            {
                FirstName = "Mikael",
                LastName = "Olsson",
                Description = @"Mikael Olsson is a professional web entrepreneur, programmer, and author. He works for an R&D company in Finland where he specializes in software development. In his spare time he writes books and creates websites that summarize various fields of interest. The books he writes are focused on teaching their subject in the most efficient way possible, by explaining only what is relevant and practical without any unnecessary repetition or theory."
            },
            new Author
            {
                FirstName = "Charles",
                LastName = "Waghmare",
                Description = @"Charles David Waghmare worked as Global Yammer Community Manager from 2011 until mid-2018 with Capgemini and previously, he was Community Manager of SAP-based communities at ATOS where he managed communities using TechnoWeb 2.0 – a Yammer-like platform. The Capgemini Yammer network, one of the largest Yammer networks, was moderated by Charles to make Yammer a wonderful experience for each Capgemini user. In short, to make Capgemini collaborate, connect, and share on business-related activities.
                    Charles had some incredible opportunities to travel with Yammer, in Paris, Amsterdam, Atlanta, Shenzhen and San Francisco to connect with Yammer customers and teams.  He was awarded  “Most Engaging” by Yammer Customer Network members in 2012. He is also Yammer Community Management Certificated."
            },
            new Author
            {
                FirstName = "Melissa",
                LastName = "Hubbard",
                Description = @"Melissa Hubbard is a Microsoft MVP and an Office 365 and SharePoint consultant specializing in collaboration solutions and automating business processes. She is a certified Project Management Professional (PMP) experienced in project management and quality assurance as well as implementing SharePoint and Office 365 solutions. She is passionate about user adoption, governance, and training. Melissa regularly blogs and speaks at events and conferences, most recently on the topics of Microsoft Teams and Flow."
            },
            new Author
            {
                FirstName = "Matthew",
                LastName = "Bailey",
                Description = @"Matthew J. Bailey is a Microsoft MVP and Microsoft Certified Trainer (MCT) for Noteworthy Technology Training, specializing in SharePoint, Office 365 (including Teams), Azure, and Power BI. He combines his business expertise and his technical knowledge to resolve corporate challenges. He is a highly regarded presenter, avid blogger, and author, most recently of The SharePoint Business Analyst Guide."
            },
            new Author
            {
                FirstName = "Manish",
                LastName = "Sharma",
                Description = @"Manish Sharma is a senior technology evangelist at Microsoft. He has an experience of 14 years with various organizations and is primarily involved in technological enhancements.He is a certified Azure Solution Architect, Cloud Data Architect, .NET Solution Developer and PMP certified project manager. He is a regular speaker in various technical conferences organized by Microsoft (FutureDecoded, Azure Conference, specialized Webinars) & Community (GIDS, Docker etc.) for Client-Server, Cloud & Data technologies."
            },
            new Author
            {
                FirstName = "Dawid",
                LastName = "Borycki",
                Description = @"Dawid Borycki is a software engineer, biomedical researcher, and an expert in several Microsoft developer technologies. He has resolved a broad range of software development challenges for device prototypes (mainly medical equipment), embedded device interfacing, and desktop and mobile programming. Dawid regularly speaks at international developers conferences and has published, cited, and presented on numerous developer topics, including web technologies, mobile/cross-platform development, wearables, embedded, and more."
            },
            new Author
            {
                FirstName = "Mathew",
                LastName = "Salvaris",
                Description = @"Mathew Salvaris, PhD is a senior data scientist at Microsoft in the Cloud and AI division, where he works with a team of data scientists and engineers building machine learning and AI solutions for external companies utilizing Microsoft's Cloud AI platform. He enlists the latest innovations in machine learning and deep learning to deliver novel solutions for real-world business problems, and to leverage learning from these engagements to help improve Microsoft's Cloud AI products. Prior to joining Microsoft, he worked as a data scientist for a fintech startup where he specialized in providing machine learning solutions. Previously, he held a postdoctoral research position at University College London in the Institute of Cognitive Neuroscience, where he used machine learning methods and electroencephalography to investigate volition. Prior to that position, he worked as a postdoctoral researcher in brain computer interfaces at the University of Essex. Mathew holds a PhD and MSc in computer science."
            },
            new Author
            {
                FirstName = "Danielle",
                LastName = "Dean",
                Description = @"Danielle Dean, PhD is a principal data science lead at Microsoft in the Cloud and AI division, where she leads a team of data scientists and engineers building artificial intelligence solutions with external companies utilizing Microsoft’s Cloud AI platform. Previously, she was a data scientist at Nokia, where she produced business value and insights from big data through data mining and statistical modeling on data-driven projects that impacted a range of businesses, products, and initiatives. She has a PhD in quantitative psychology from the University of North Carolina at Chapel Hill, where she studied the application of multi-level event history models to understand the timing and processes leading to events between dyads within social networks."
            },
            new Author
            {
                FirstName = "Wee Hyong",
                LastName = "Tok",
                Description = @"Wee Hyong Tok, PhD is a principal data science manager at Microsoft in the Cloud and AI division. He leads the AI for Earth Engineering and Data Science team, where his team of data scientists and engineers are working to advance the boundaries of state-of-art deep learning algorithms and systems. His team works extensively with deep learning frameworks, ranging from TensorFlow to CNTK, Keras, and PyTorch. He has worn many hats in his career as developer, program/product manager, data scientist, researcher, and strategist. Throughout his career, he has been a trusted advisor to the C-suite, from Fortune 500 companies to startups. He co-authored one of the first books on Azure machine learning, Predictive Analytics Using Azure Machine Learning, and authored another demonstrating how database professionals can do AI with databases, Doing Data Science with SQL Server. He has a PhD in computer science from the National University of Singapore, where he studied progressive join algorithms for data streaming systems."
            },
            new Author
            {
                FirstName = "Nishith",
                LastName = "Pathak",
                Description = @"Nishith Pathak is Vice President at Accenture Technology Labs, a Microsoft Regional Director (RD), and an artificial intelligence Microsoft MVP. He helps Fortune 100 companies design and architect next-generation solutions that incorporate AI, ML, cognitive services, Blockchain, and more. He sits on several technical advisory boards across the globe, and is author of multiple books, most recently Artificial Intelligence for .NET (Apress, 2017). He is an internationally acclaimed speaker on technologies such as AI and Blockchain, and regularly speaks at various technical conferences."
            },
            new Author
            {
                FirstName = "Anurag",
                LastName = "Bhandari",
                Description = @"Anurag Bhandari is Senior Researcher at Accenture Technology Labs, where he designs next-generation AI, ML, and IoT solutions for clients. He is a member of the Association of Computing Machinery (ACM), and regularly speaks at research and tech conferences. He also specializes in mobile and web development, and lives on the leading edge of these technologies. He is an ardent open-source evangelist whose love for free software helped him found the Granular Linux project 11 years ago."
            },
            new Author
            {
                FirstName = "Ambi Karthikeyan",
                LastName = "Shijimol",
                Description = @"Shijimol Ambi Karthikeyan currently works as a cloud consultant with Microsoft. She has 12+ years' experience in IT and specializes in datacenter management, virtualization, and cloud computing technologies. She started her career with EY IT services in the datacenter management team managing complex virtualized production datacenters. She has expertise in managing VMware and Hyper-V virtualization stacks and Windows/Linux server technologies. She has also worked on devops CI/CD implementation projects using tools such as TeamCity, Jenkins, Git, TortoiseSVN, Mercurial, and Selenium. She later moved on to cloud computing and gained expertise in Windows Azure, focusing on Azure IaaS, backup/DR, and automation. She holds industry standard certifications in technologies including Microsoft Azure, Windows Server, and VMware. She also holds ITIL and TOGAF9 certifications.  She has previously written a book on Azure automation."
            },
            new Author
            {
                FirstName = "Szymon",
                LastName = "Rozga",
                Description = @"Szymon has worked in the software development space for over 14 years taking a shot at everything from Rails to C to Java to Python to .Net. He found a passion working on Windows Desktop trading applications on Wall Street with a focus on the front end user experience. The interest in attention to user interface details would take him on a tour of the different UI technologies on the Windows, Web and iOS/Android platforms. He enjoys leading deep discovery sessions and scoping solutions for his clients. Szymon has managed teams of engineers on a variety of projects and can be commonly found leading engagements, mentoring junior engineers (another passion of his) and collaborating on building team engineering capacity in emerging technologies. Starting in 2016, Szymon has spent all his time dedicated to building bots for clients whether the bot runs on a mobile app, the web or a messaging platform. He has built a practice around the technology and is involved in a handful of bot projects at any time. He has a passion for paying attention to conversational experience details and is a big proponent of getting your bot to do a few tasks really well. He has a deep interest in all things Machine Learning and Artificial Intelligence. In his spare time he enjoys walking in nature, relaxing on the beach, playing guitar and spending time with his wife Kimberly, his son Theodore and his dog Chelsea. He’s an avid reader, especially of things completely unrelated to software development."
            },
            new Author
            {
                FirstName = "Balu",
                LastName = "Ilag",
                Description = @"Balu Ilag is a five times Microsoft MVP, MCSE: Communication and MCITP certified professional. He has been working as a consultant with over 12 years of experience working with Microsoft Unified Communication solutions including OCS, Lync and Skype for Business. He also authors administrative blog on Unified Communication and has participated in writing Getting Started with Microsoft Teams book and multiple admin guide for Microsoft TechNet Gallery."
            },
            new Author
            {
                FirstName = "Jason",
                LastName = "Gilmore",
                Description = @"W. Jason Gilmore is a web developer and business consultant with more than 15 years of experience helping companies large and small build amazing software solutions. He has been teaching developers from around the world about web development for over a decade, having written six books, including the bestselling Beginning PHP and MySQL, Fourth Edition and Easy PHP Websites with the Zend Framework, Second Edition .Over the years Jason has been published more than 300 times within popular publications such as Developer.com, PHPBuilder.com, JSMag, and Linux Magazine, and instructed hundreds of students in the United States and Europe. He s recently led the successful development and deployment of a 10,000+ product e-commerce project, and is currently working on a variety of new e-commerce initiatives. Jason is cofounder of the wildly popular CodeMash Conference, the largest multi-day developer event in the Midwest."
            },
            new Author
            {
                FirstName = "Frank",
                LastName = "Kromann",
                Description = @"Frank Kromann has spent more than 30 years solving business problems using software and technology. Since the introduction of the first web browser he has developed systems with web technology on Unix, Linux, Windows, and Mac platforms, with the primary focus on PHP, JavaScript, C/C++, and other languages. He has contributed several PHP extensions over the years and has been a member of the PHP development team since 1997. Previous publications included several articles in PHP Magazine and he was the coauthor of PHP and MySQL Recipes (Apress, 2016). Frank has held managing positions for more than 20 years, leading both smaller and larger teams in development and implementation of business systems and processes utilizing databases and programming. Currently he is an Engineering Manager at Panasonic Avionics and the CEO and cofounder of Web by Pixel, Inc. Kromann holds a Master of Science degree in Electrical Engineering from the Technical University of Denmark."
            },
            new Author
            {
                FirstName = "Alex",
                LastName = "Libby",
                Description = @"Alex Libby is an A/B testing developer and seasoned computer book author, who hails from England. His passion for all things Open Source dates back to the days of his degree studies, where he first came across web development, and has been hooked ever since. His daily work involves extensive use of JavaScript, HTML and CSS to manipulate existing website content; Alex enjoys tinkering with different open source libraries to see how they work. He has spent a stint maintaining the jQuery Tools library, and enjoys writing about Open Source technologies, principally for front end UI development."
            },
            new Author
            {
                FirstName = "Wolfgang",
                LastName = "Loder",
                Description = @"Wolfgang Loder has been programming software since the 1980s. He successfully rejected all calls for management roles and remained hands-on until now.  His journey went from Assembler and C to C++ and Java to C# and F# and JavaScript, from Waterfall To Agile, from Imperative to Declarative and other paradigm changes too many to list and remember. Most of his career Wolfgang was a contracting 'Enterprise Developer', so the introduction of 'new' languages, frameworks and concepts is very slow in this field. Once he decided to develop his own products he was free of such constraints and ventured into all sorts of paradigms, be it NoSQL or functional and evaluating all the latest ideas, crazy or not. In other words, he has fun developing software. Wolfgang was born in Vienna and lives in Austria and Kenya."
            },
            new Author
            {
                FirstName = "Jeanine",
                LastName = "Meyer",
                Description = @"Jeanine Meyer is a Professor at Purchase College/SUNY and past Coordinator of the Mathematics/Computer Science Board of Study.  Before Purchase, she taught at Pace University and worked at IBM Research and other parts of IBM and at other companies. She is the author of 4 books and co-author of 3 more on topics ranging from educational uses of multimedia, programming (two published by Apress), databases and number theory. She earned a PhD in computer science at the Courant Institute at New York University, an MA in mathematics at Columbia, and a SB (the college used the Latin form) in mathematics from the University of Chicago.  She is a member of Phi Beta Kappa, Sigma Xi, Association of Women in Science, Association of Computing Machinery, and a featured reviewer for ACM Computing Reviews. Jeanine is trying but remains a beginner at Spanish and piano."
            },
            new Author
            {
                FirstName = "Fernando",
                LastName = "Doglio",
                Description = @"Fernando Doglio has been working as a Web Developer for the past 10 years. In that time, he's come to love the web, and has had the opportunity of working with most of the leading technologies at the time, suchs as PHP, Ruby on Rails, MySQL, and Node. js, Angular.js, AJAX, REST APIs and others. In his spare time, he likes to tinker and learn new things, which is why his Github account keeps getting new repos every month. He's also a big Open Source supporter, trying to convert new people into it. He can be contacted on twitter at: @deleteman123. When not programming, he can be seen spending time with his family."
            },
            new Author
            {
                FirstName = "Chander",
                LastName = "Dhall",
                Description = @"Chander Dhall is a Microsoft MVP, Tech Ed Speaker, ASP.NET Insider, Web API Advisor, Dev Chair - DevConnections professional software architect/lead developer, trainer, INETA speaker, open source contributor, community leader and organizer with years of experience in enterprise software development."
            },
            new Author
            {
                FirstName = "Grant",
                LastName = "Fritchey",
                Description = @"Grant Fritchey, Microsoft Data Platform MVP, has more than 20 years of experience in IT. That time was spent in technical support, development, and database administration. He currently works as Product Evangelist at Red Gate Software. Grant writes articles for publication at SQL Server Central and Simple-Talk. He has published books, including SQL Server Execution Plans and SQL Server 2012 Query Performance Tuning (Apress). He has written chapters for Beginning SQL Server 2012 Administration (Apress), SQL Server Team-based Development, SQL Server MVP Deep Dives Volume 2, Pro SQL Server 2012 Practices (Apress), and Expert Performance Indexing in SQL Server (Apress). Grant currently serves as President on the Board of Directors of the PASS organization, the leading source of educational content and training on the Microsoft Data Platform."
            },
            new Author
            {
                FirstName = "Charles",
                LastName = "Bell",
                Description = @"Charles Bell conducts research in emerging technologies. He is a member of the Oracle MySQL development team and is a senior software developer for the MySQL Enterprise backup team. He lives in a small town in rural Virginia with his loving wife. He received his Doctor of Philosophy degree in engineering from Virginia Commonwealth University in 2005. 
                    Charles is an expert in the database field and has extensive knowledge and experience in software development and systems engineering. His research interests include 3D printers, microcontrollers, 3D printing, database systems, software engineering, high availability systems, cloud, and sensor networks. He spends his limited free time as a practicing Maker, focusing on microcontroller projects and refinement of 3D printers."
            },
            new Author
            {
                FirstName = "Bikramaditya",
                LastName = "Singhal",
                Description = @"Bikramaditya Singhal is a Blockchain expert and AI practitioner with experience working in multiple industries. He is proficient in Blockchain, Bitcoin, Ethereum, Hyperledger, cryptography, cyber security, and data science. He has extensive experience in training and consulting on Blockchain and has designed many Blockchain solutions. He worked with companies such as WISeKey, Tech Mahindra, Microsoft India, Broadridge, Chelsio Communications, and he also co-founded a company named Mund Consulting that focuses on big data analytics and artificial intelligence. He is an active speaker at various conferences, summits, and meetups. He has also authored a book entitled Spark for Data Science."
            },
            new Author
            {
                FirstName = "Gautam",
                LastName = "Dhameja",
                Description = @"Gautam Dhameja is a blockchain application consultant based out of Berlin, Germany. For most of this decade, he has been developing and delivering enterprise software including Web & Mobile apps, Cloud-based hyper-scale IoT solutions, and more recently, Blockchain-based decentralized applications (DApps). He possesses a deep understanding of the decentralized stack, cloud solutions architecture and object-oriented design. His areas of expertise include Blockchain, Cloud-based enterprise solutions, Internet of Things, Distributed Systems and Native & Hybrid Mobile apps. He is also an active blogger and speaker and regularly speaks at tech conferences and events."
            },
            new Author
            {
                FirstName = "Priyansu",
                LastName = "Panda",
                Description = @"Priyansu Panda is a research engineer at Underwriters Laboratories, Bangalore, India. He has worked with other IT companies such as Broadridge, Infosys Limited, and Tech Mahindra. His areas of expertise include Blockchain, Bitcoin, Ethereum, Hyperledger, game theory, IoT, and artificial intelligence. Priyansu's current research is on building next-gen applications leveraging Blockchain, IoT, and AI. His major research interests include building Decentralized Autonomous Organizations (DAO), and the security, scalability, and consensus of Blockchains."
            });

            //seeding categories
            context.Categories.AddOrUpdate(new Category
            {
                Name = "Microsoft and .NET",
                Description = @"Explore titles that are among the best available relevant and practical guides out there for today’s ASP.NET and Windows developers, for IT professionals working with a broad suite of Microsoft products from the Microsoft Azure cloud to Office 365 and SharePoint, and for business users of Excel, Windows, and more.
                    Work with powerful out-of-the-box business solutions, build apps for desktop, web, and mobile devices, manage thousands of concurrent users in the cloud, or automate any part of it with Windows PowerShell."
            },
            new Category
            {
                Name = "Database Management",
                Description = @"Explore our titles on NoSQL and relational database technologies. Our books provide extensive coverage of leading database engines from Oracle, MySQL, and Microsoft SQL Server, as well as document-oriented databases from Couchbase Server to MongoDB.
                    Database administrators, SQL developers, systems analysts, and other data professionals will find expert advice on the art and science of database design, management, performance, scaling, good practice and more, to help their projects succeed."
            },
            new Category
            {
                Name = "Web Development",
                Description = @"Encompassing HTML, CSS, JavaScript, PHP, WordPress, Ruby Programming, and more, our web development books teach you how to develop and design successfully on the web. Whether you are a beginner looking to learn a new skill, or a seasoned web developer looking for an expert guide to JavaScript, here you can find the best title for you."
            });

                context.SaveChanges();

                //setting up relationships
                Book book = context.Books.SingleOrDefault(x => x.Title == "Beginning Xamarin Development for the Mac");
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Dawid" && x.LastName == "Borycki"));
                book.Category = context.Categories.SingleOrDefault(x => x.Name == "Microsoft and .NET");

                book = context.Books.SingleOrDefault(x => x.Title == "The Business of Machine Learning");
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Josh" && x.LastName == "Holmes"));
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Mike" && x.LastName == "Lanzetta"));
                book.Category = context.Categories.SingleOrDefault(x => x.Name == "Microsoft and .NET");

                book = context.Books.SingleOrDefault(x => x.Title == "C# 7 Quick Syntax Reference");
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Mikael" && x.LastName == "Olsson"));
                book.Category = context.Categories.SingleOrDefault(x => x.Name == "Microsoft and .NET");

                book = context.Books.SingleOrDefault(x => x.Title == "Yammer");
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Charles" && x.LastName == "Waghmare"));
                book.Category = context.Categories.SingleOrDefault(x => x.Name == "Microsoft and .NET");

                book = context.Books.SingleOrDefault(x => x.Title == "Mastering Microsoft Teams");
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Melissa" && x.LastName == "Hubbard"));
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Matthew" && x.LastName == "Bailey"));
                book.Category = context.Categories.SingleOrDefault(x => x.Name == "Microsoft and .NET");

                book = context.Books.SingleOrDefault(x => x.Title == "Cosmos DB for MongoDB Developers");
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Manish" && x.LastName == "Sharma"));
                book.Category = context.Categories.SingleOrDefault(x => x.Name == "Microsoft and .NET");

                book = context.Books.SingleOrDefault(x => x.Title == "Deep Learning with Azure");
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Mathew" && x.LastName == "Salvaris"));
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Danielle" && x.LastName == "Dean"));
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Wee Hyong" && x.LastName == "Tok"));
                book.Category = context.Categories.SingleOrDefault(x => x.Name == "Microsoft and .NET");

                book = context.Books.SingleOrDefault(x => x.Title == "IoT, AI, and Blockchain for .NET");
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Nishith" && x.LastName == "Pathak"));
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Anurag" && x.LastName == "Bhandari"));
                book.Category = context.Categories.SingleOrDefault(x => x.Name == "Microsoft and .NET");

                book = context.Books.SingleOrDefault(x => x.Title == "Practical Microsoft Azure IaaS");
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Ambi Karthikeyan" && x.LastName == "Shijimol"));
                book.Category = context.Categories.SingleOrDefault(x => x.Name == "Microsoft and .NET");

                book = context.Books.SingleOrDefault(x => x.Title == "Practical Bot Development");
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Szymon" && x.LastName == "Rozga"));
                book.Category = context.Categories.SingleOrDefault(x => x.Name == "Microsoft and .NET");

                book = context.Books.SingleOrDefault(x => x.Title == "Introducing Microsoft Teams");
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Balu" && x.LastName == "Ilag"));
                book.Category = context.Categories.SingleOrDefault(x => x.Name == "Microsoft and .NET");

                book = context.Books.SingleOrDefault(x => x.Title == "Beginning PHP and MySQL");
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Jason" && x.LastName == "Gilmore"));
                book.Category = context.Categories.SingleOrDefault(x => x.Name == "Web Development");

                book = context.Books.SingleOrDefault(x => x.Title == "Beginning SVG");
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Alex" && x.LastName == "Libby"));
                book.Category = context.Categories.SingleOrDefault(x => x.Name == "Web Development");

                book = context.Books.SingleOrDefault(x => x.Title == "Web Applications with Elm");
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Wolfgang" && x.LastName == "Loder"));
                book.Category = context.Categories.SingleOrDefault(x => x.Name == "Web Development");

                book = context.Books.SingleOrDefault(x => x.Title == "HTML5 and JavaScript Projects");
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Jeanine" && x.LastName == "Meyer"));
                book.Category = context.Categories.SingleOrDefault(x => x.Name == "Web Development");

                book = context.Books.SingleOrDefault(x => x.Title == "REST API Development with Node.js");
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Fernando" && x.LastName == "Doglio"));
                book.Category = context.Categories.SingleOrDefault(x => x.Name == "Web Development");

                book = context.Books.SingleOrDefault(x => x.Title == "Scalability Patterns");
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Chander" && x.LastName == "Dhall"));
                book.Category = context.Categories.SingleOrDefault(x => x.Name == "Web Development");

                book = context.Books.SingleOrDefault(x => x.Title == "SQL Server 2017 Query Performance Tuning");
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Grant" && x.LastName == "Fritchey"));
                book.Category = context.Categories.SingleOrDefault(x => x.Name == "Database Management");

                book = context.Books.SingleOrDefault(x => x.Title == "Introducing InnoDB Cluster");
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Charles" && x.LastName == "Bell"));
                book.Category = context.Categories.SingleOrDefault(x => x.Name == "Database Management");

                book = context.Books.SingleOrDefault(x => x.Title == "Beginning Blockchain");
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Bikramaditya" && x.LastName == "Singhal"));
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Gautam" && x.LastName == "Dhameja"));
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Priyansu" && x.LastName == "Panda"));
                book.Category = context.Categories.SingleOrDefault(x => x.Name == "Database Management");

                book = context.Books.SingleOrDefault(x => x.Title == "Introducing the MySQL 8 Document Store");
                book.Authors.Add(context.Authors.SingleOrDefault(x => x.FirstName == "Charles" && x.LastName == "Bell"));
                book.Category = context.Categories.SingleOrDefault(x => x.Name == "Database Management");

                context.SaveChanges();
            }
        }
    }
}
