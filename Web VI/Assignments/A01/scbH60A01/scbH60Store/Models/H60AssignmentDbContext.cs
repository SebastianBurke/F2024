﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace scbH60Store.Models;

public partial class H60AssignmentDbContext : DbContext
{
    public H60AssignmentDbContext()
    {
    }

    public H60AssignmentDbContext(DbContextOptions<H60AssignmentDbContext> options)
        : base(options)
    {
        //Database.EnsureCreated();  // this has to be commented out for migrations to be run
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<GlobalSettings> GlobalSettings { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=cssql.cegep-heritage.qc.ca;Database=H60AssignmentDB_scb;User Id=SCANALESBURKE;Password=password;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.HasIndex(e => e.ProdCatId, "IX_Product_ProdCatId");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.BuyPrice).HasColumnType("numeric(8, 2)");
            entity.Property(e => e.Description)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SellPrice).HasColumnType("numeric(8, 2)");

            entity.HasOne(d => d.ProdCat).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProdCatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_ProductCategory");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.ToTable("ProductCategory");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.ProdCat)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        // Seed data
        modelBuilder.Entity<ProductCategory>().HasData(
            new ProductCategory { CategoryId = 1, ProdCat = "DC Comics", ImageUrl = "/images/dc_comics.jpg" },
            new ProductCategory { CategoryId = 2, ProdCat = "Marvel", ImageUrl = "/images/marvel_comics.jpg" },
            new ProductCategory { CategoryId = 3, ProdCat = "Image Comics", ImageUrl = "/images/image_comics.jpg" },
            new ProductCategory { CategoryId = 4, ProdCat = "Shonen", ImageUrl = "/images/shonen_manga.jpg" },
            new ProductCategory { CategoryId = 5, ProdCat = "Seinen", ImageUrl = "/images/seinen_manga.jpg" }
        );

        modelBuilder.Entity<Product>().HasData(
            // DC Comics Category Products
            new Product { ProductId = 1, ProdCatId = 1, Description = "All-Star Batman: The Deluxe Edition HC", Manufacturer = "Scott Snyder", Stock = 100, BuyPrice = 10.00m, SellPrice = 25.00m, EmployeeNotes = "Acclaimed writer Scott Snyder joins forces with legendary artists including John Romita Jr. and BATMAN: THE BLACK MIRROR collaborator, Jock, to bring fans a Batman tale like no other. Snyder and team dive into Batman’s complicated relationships with some of the greatest villains of popular culture, with a modern sensibility.\r\n\r\nWhat does a road trip with disgraced district attorney Harvey Dent, now the villainous Two-Face, look like? Then dive into chilling tales featuring Mr. Freeze and Poison Ivy.  Finally, the mastermind behind these villains and there twisted end goal will be revealed!", ImageUrl = "/images/all_star_batman_the_deluxe_edition.jpg" },
            new Product { ProductId = 2, ProdCatId = 1, Description = "Wesley Dodds: The Sandman TP", Manufacturer = "Robert Venditti, Riley Rossmo, Ivan Plascencia, Tom Napolitano", Stock = 140, BuyPrice = 8.00m, SellPrice = 17.00m, EmployeeNotes = "Wesley Dodds' dream of a better world is now a nightmare, as DC’s original Sandman returns in a gripping new noir mystery!\r\n\r\nNo one escapes the Sandman's dark dreams, not even Wesley Dodds himself. After years of testing and experimentation, Wesley perfected his sleep gas as the optimal weapon to fight crime without causing undue harm. But when his journal detailing all his failed and far more deadly formulas is stolen, the Sandman must hunt down the thief and the people in the shadows pulling the strings!\r\n\r\nCan Wesley solve the mystery of who broke into his home before these noxious weapons are unleashed on the world, or is Sandman fated to fade away into the mists?\r\n\r\nWesley Dodds: The Sandman is written by comics superstar Robert Venditti (Superman ’78) and vividly drawn by fan-favorite artist Riley Rossmo (Harley Quinn). A bold and thoroughly modern exploration of one of comics’ most classic characters, Wesley Dodds: The Sandman is part of DC’s The New Golden Age initiative, along with Jay Garrick: The Flash and Alan Scott: The Green Lantern.", ImageUrl = "/images/the_sandman.jpg" },
            new Product { ProductId = 3, ProdCatId = 1, Description = "Nightwing: Uncovered #1", Manufacturer = "Ivan Cohen, Dexter Soy", Stock = 70, BuyPrice = 9.00m, SellPrice = 13.00m, EmployeeNotes = "Dick Grayson is front and center in a stunning collection of some of the most compelling cover art to grace his solo title over the years!Dick Grayson is front and center in a stunning collection of some of the most compelling cover art to grace his solo title over the years!", ImageUrl = "/images/nightwing_uncovered_1.jpg" },
            new Product { ProductId = 4, ProdCatId = 1, Description = "Batman: The Long Halloween - The Last Halloween #0", Manufacturer = "Jeph Loeb, Tim Sale, Brennan Wagner, Richard Starkings, Ben Abernathy", Stock = 80, BuyPrice = 12.00m, SellPrice = 18.00m, EmployeeNotes = "A NEW EDITION OF THE FINAL COLLABORATION BETWEEN JEPH LOEB AND TIM SALE! Prelude to Batman The Long Halloween: The Last Halloween! You thought you knew the whole story of Batman: The Long Halloween... you were wrong! Reprinting the final collaboration between legendary creators Jeph Loeb and Tim Sale, this special uncovers a deadly mystery that could destroy Batman, Commissioner Gordon, Two-Face, and... well, that would be telling, wouldn't it? Don't miss out on this special reprint of the prelude to THE LAST HALLOWEEN.", ImageUrl = "/images/batman_the_long_halloween_the_last_halloween_0.jpg" },
            new Product { ProductId = 5, ProdCatId = 1, Description = "Superman #18", Manufacturer = "Joshua Williamson, Jamal Campbell", Stock = 80, BuyPrice = 12.00m, SellPrice = 18.00m, EmployeeNotes = "ABSOLUTE POWER TIE-IN! Waller has the powerless heroes of the DC Universe on the ropes! Can the powerless Superman and Zatanna find the mystical map to the Dark Roads in time to get some major back-up?! Lex Luthor, Lois Lane, Mercy, Jimmy, and Silver Banshee are on the run from the superpowered Amazos but find themselves pulled into a battle for the soul of Metropolis! Don't miss the shocking cliffhanger that impacts the future of the Superman titles!", ImageUrl = "/images/superman_18.jpg" },
            new Product { ProductId = 6, ProdCatId = 1, Description = "Batman Day 2024: Batman / Elmer Fudd Special Noir #1", Manufacturer = "Tom King, Lee Weeks, Deron Bennett", Stock = 80, BuyPrice = 12.00m, SellPrice = 18.00m, EmployeeNotes = "", ImageUrl = "/images/batman_day_2024_batman_elmer_fudd_special_noir_1.jpg" },
            new Product { ProductId = 7, ProdCatId = 1, Description = "Azrael #20", Manufacturer = "Dennis O'Neil, Tom Grindberg, James Pascoe, Ken Bruzenak, Chuck Kim", Stock = 80, BuyPrice = 12.00m, SellPrice = 18.00m, EmployeeNotes = "The Orchid brothers find a way to end their blood feud: destroy Azrael! Trapped on an island with the murderous siblings, Jean-Paul Valley is shocked to find that, at the most dire moment, his Azrael persona deserts him!", ImageUrl = "/images/azrael_20.jpg" },
            new Product { ProductId = 8, ProdCatId = 1, Description = "American Vampire Book One: DC Compact Comics Edition TP", Manufacturer = "Scott Snyder, Rafael Albuquerque, Stephen King, Dave McCaig, Steve Wands", Stock = 80, BuyPrice = 12.00m, SellPrice = 18.00m, EmployeeNotes = "Chronicling the history of a new breed of vampire, AMERICAN VAMPIRE by the legendary Scott Snyder and Stephen King is a fresh look at an old monster — a generational epic showcasing the bloodlust that lay hidden beneath America's most distinctive eras. Cunning, ruthless, and rattlesnake mean, Skinner Sweet is a thoroughly corrupt gunslinger. When European vampires come to the American Old West, they turn Skinner into a true monster: the very first American vampire.\r\n\r\nSkinner becomes something entirely new — a stronger breed of vampire immune to sunlight, who hates every last one of his aristocratic European ancestors.\r\n\r\nFollow this dark symbol of the New World's bloody path as he moves through American history's most distinctive eras — from the Wild West in the 1880s to the glamorous classic Hollywood of the 1920s to mobster-run Las Vegas in the 1930s, and beyond.\r\n\r\nBut as Skinner's war with his predecessors inspires a mysterious society to rise and fight them both, his most upsetting decision might involve the first person he chooses to join his vampiric ranks: a struggling young movie star named Pearl Jones.", ImageUrl = "/images/american_vampire_1.jpg" },

            // Marvel Category Products
            new Product { ProductId = 9, ProdCatId = 2, Description = "Wolverine (2024) #1", Manufacturer = "Saladin Ahmed, Martin Coccolo", Stock = 120, BuyPrice = 5.00m, SellPrice = 14.00m, EmployeeNotes = "THE LEGEND BEGINS ANEW IN THE ADAMANTIUM-TOUGH NEW ONGOING SERIES! There's a killer in the woods - and as WOLVERINE's attempt at piece is shattered, an OLD ENEMY will re-emerge as a NEW VILLAIN rises who will bring LOGAN to the brink of his berserker rage. But NIGHTCRAWLER knows his old friend is capable of doing what's right, and before long, Logan will have to unleash his claws, push his healing factor to the limit and demonstrate he's the best there is at what he does once and for all - nice be damned! The legendary WOLVERINE ongoing series kicks off anew with the superstar creative team of Saladin Ahmed (DAREDEVIL, MS. MARVEL) and Martín Cóccolo (DEADPOOL, IMMORTAL THOR) beginning their epic journey with Logan! Collector's Note: A key FIRST APPEARANCE and a major addition to the lore of Wolverine in this issue!", ImageUrl = "/images/Wolverine_2024_1.jpg" },
            new Product { ProductId = 10, ProdCatId = 2, Description = "Wolverine (2024) #2", Manufacturer = "Saladin Ahmed, Martin Coccolo", Stock = 120, BuyPrice = 5.00m, SellPrice = 14.00m, EmployeeNotes = "WHERE GOES THE WENDIGO?! Who stalks WOLVERINE in the Canadian North? And what mysterious designs does the WENDIGO have on the Best There Is? Logan just wants to be left alone, but a war on two fronts will evolve with an unexpected turn! Don't miss the debut of the all-new Wendigo, as the secret it hides will shape Wolverine's mission…", ImageUrl = "/images/Wolverine_2024_2.jpg" },
            new Product { ProductId = 11, ProdCatId = 2, Description = "Wolverine (2024) #3", Manufacturer = "Saladin Ahmed, Martin Coccolo", Stock = 120, BuyPrice = 5.00m, SellPrice = 14.00m, EmployeeNotes = "DEPARTMENT H GOES HUNTING! Canada's DEPARTMENT H has their sights trained once more on WOLVERINE! Years ago, they played a role in WEAPON X and LOGAN's first assignment, but what else are they hunting now that mutants are hated and feared more than ever? Meanwhile, Wolverine's UNLIKELY ALLY may have just killed an innocent…and OLD ENEMIES of Wolverine's gather as more sinister machinations unfurl… A key issue, as the ALL-NEW villain moving against Wolverine comes into sharper focus…", ImageUrl = "/images/Wolverine_2024_3.jpg" },
            new Product { ProductId = 12, ProdCatId = 2, Description = "Spider-Man: Reign 2 (2024) #1", Manufacturer = "Kaare Andrews", Stock = 130, BuyPrice = 6.00m, SellPrice = 15.00m, EmployeeNotes = "BACK TO THE (AMAZING SPIDER-MAN'S) FUTURE! Award-winning writer/artist Kaare Andrews returns to the world of SPIDER-MAN'S dystopian future in this sequel to the landmark, genre-defying SPIDER-MAN: REIGN! And who is the new BLACK CAT?! What tragedies and triumphs await this older, grizzled Peter Parker? Peter isn't the only one who aged… wait until you see what happened to MILES MORALES!", ImageUrl = "/images/spider_man_reign_2_2024_1.jpg" },
            new Product { ProductId = 13, ProdCatId = 2, Description = "Spider-Man: Reign 2 (2024) #2", Manufacturer = "Kaare Andrews", Stock = 130, BuyPrice = 6.00m, SellPrice = 15.00m, EmployeeNotes = "Old Man Peter returns to the past! Can he save the future and, more importantly, Mary Jane? Not if MILES MORALES has anything to say about it. You don't want to miss the latest chapter of the most notorious Spidey story ever told!", ImageUrl = "/images/spider_man_reign_2_2024_2.jpg" },
            new Product { ProductId = 14, ProdCatId = 2, Description = "Spider-Man: Reign 2 (2024) #3", Manufacturer = "Kaare Andrews", Stock = 130, BuyPrice = 6.00m, SellPrice = 15.00m, EmployeeNotes = "Old Man Peter Parker is lashing out and making wildly bad decisions, but what else is new? Well, now he's got Miles Morales after him (and Miles is no spring chicken himself). The Spider-War is fought, and the whole of existence may very well be at stake as time and space get pulled to the brink!", ImageUrl = "/images/spider_man_reign_2_2024_3.jpg" },
            new Product { ProductId = 15, ProdCatId = 2, Description = "Vision (2015) #1", Manufacturer = "Tom King, Gabriel Hernandez Walta, Mike Del Mundo", Stock = 60, BuyPrice = 4.00m, SellPrice = 12.00m, EmployeeNotes = "The Vision wants to be human, and what's more human than family? He goes to the laboratory where he was created, where Ultron molded him into a weapon, where he first rebelled against his given destiny, where he first imagined that he could be more, that he could be good, that he could be a man, a normal, ordinary man. And he builds them. A wife, Virginia. Two teenage twins, Viv and Vin. They look like him. They have his powers. They share his grandest ambition or perhaps obsession: the unrelenting need to be ordinary. Behold The Visions! They're the family next door, and they have the power to kill us all. What could possibly go wrong?", ImageUrl = "/images/vision_2015_1.jpg" },
            new Product { ProductId = 16, ProdCatId = 2, Description = "Vision (2015) #2", Manufacturer = "Tom King, Gabriel Hernandez Walta, Mike Del Mundo", Stock = 60, BuyPrice = 4.00m, SellPrice = 12.00m, EmployeeNotes = "After the stunning, heart-stopping events of Vision #1, the Avengers will never be the same. The Vision and his family attempt to cope with these events in their own, unique way. Though they put on a happy face, each of them can feel their anger growing, blistering, tearing them apart. Will the Visions be able to hold together, or will that anger destroy them and the world around them?", ImageUrl = "/images/vision_2015_2.jpg" },
            new Product { ProductId = 17, ProdCatId = 2, Description = "Vision (2015) #3", Manufacturer = "Tom King, Gabriel Hernandez Walta, Mike Del Mundo", Stock = 60, BuyPrice = 4.00m, SellPrice = 12.00m, EmployeeNotes = "A house attacked. A daughter dying. An old, dead friend screaming out in pain. This wasn't how it was supposed to go. The Vision created his family to be normal. This isn't normal. This is terrifying. And it's just the beginning. The epic tale of Vision and his family continues as he fights to remain ordinary, and that fight starts to tear his ordinary world apart.", ImageUrl = "/images/vision_2015_3.jpg" },
            new Product { ProductId = 18, ProdCatId = 2, Description = "Deadpool #6", Manufacturer = "Cody Ziglar, Rogê Antônio", Stock = 80, BuyPrice = 12.00m, SellPrice = 18.00m, EmployeeNotes = "Wade Wilson triumphed against Death Grip! This is the first issue of a new arc and killing Deadpool NOW would be an INSANE thing to do. Which is exactly why we’re doing it.", ImageUrl = "/images/deadpool_6.jpg" },

            // Image Comics Category Products
            new Product { ProductId = 19, ProdCatId = 3, Description = "Invincible, Vol. 1 (TBP)", Manufacturer = "Robert Kirkman, Bill Crabtree, Cory Walker", Stock = 160, BuyPrice = 15.00m, SellPrice = 30.00m, EmployeeNotes = "Mark Grayson is just like most everyone else his age. Except his father is the most powerful superhero on the planet-Omni-Man. When Mark develops powers of his own it's a dream come true. But living up to his father's legacy is only the beginning of Mark's problems...", ImageUrl = "/images/invincible_1.jpg" },
            new Product { ProductId = 20, ProdCatId = 3, Description = "Invincible #144", Manufacturer = "Robert Kirkman, Nathan Fairbairn, Cory Walker", Stock = 120, BuyPrice = 12.00m, SellPrice = 20.00m, EmployeeNotes = "\"THE END OF ALL THINGS,\" Conclusion\r\n\r\nFinal issue.\r\n\r\nEverything since issue one has been building to this. Nothing can prepare you.", ImageUrl = "/images/invincible_144.jpg" },
            new Product { ProductId = 21, ProdCatId = 3, Description = "Geiger (2024) #1", Manufacturer = "Geoff Johns, Gary Frank, Brand Anderson", Stock = 120, BuyPrice = 12.00m, SellPrice = 20.00m, EmployeeNotes = "SERIES PREMIERE\r\nIT ALL STARTS HERE! The critically acclaimed team of storytellers GEOFF JOHNS and GARY FRANK (GEIGER: GROUND ZERO, Doomsday Clock) return to the nuclear wasteland of their bestselling GEIGER for an ALL-NEW ONGOING series starring the violent and unpredictable GLOWING MAN!\r\nLeaving his home behind, Tariq Geiger now walks the radioactive roads of the former United States with his two-headed wolf Barney. But as his enemies doggedly pursue him, Geiger discovers salvation from the unlikeliest of foes. But what secrets does this potential ally hold that could help Geiger? And exactly how many people are after The Glowing Man… and why?\r\nDon’t miss this vital, action-packed chapter in the shared universe of THE UNNAMED saga and the momentous Ghost Machine rollout!", ImageUrl = "/images/geiger_1.jpg" },
            new Product { ProductId = 22, ProdCatId = 3, Description = "Geiger (2024) #3", Manufacturer = "Geoff Johns, Gary Frank, Brand Anderson", Stock = 80, BuyPrice = 18.00m, SellPrice = 20.00m, EmployeeNotes = "Tariq Geiger surrounds himself with some dangerous friends. His two-headed wolf Barney bears the trauma of the fateful night that Geiger found him. And he and Geiger’s surprising new companion try to atone for a life of unfettered violence and brutality. But even between the three of them, they are no match for the many threats in pursuit. Plus, the return of Junkyard Joe!", ImageUrl = "/images/geiger_3.jpg" },
            new Product { ProductId = 23, ProdCatId = 3, Description = "Geiger (2024) #5", Manufacturer = "Geoff Johns, Gary Frank, Brand Anderson", Stock = 90, BuyPrice = 16.00m, SellPrice = 20.00m, EmployeeNotes = "This is it! The first four issues of this new series have all pointed toward this final battle between Geiger and The Electrician! But it might be a quick one: The Electrician’s clever trap hits Geiger's greatest weakness, and his friends are helpless to halt it. Also: the final fate of Barney, the two-headed mutant wolf!", ImageUrl = "/images/geiger_5.jpg" },
            new Product { ProductId = 24, ProdCatId = 3, Description = "KING SPAWN, VOL. 1 TP", Manufacturer = "Sean Lewis, Todd McFarlane", Stock = 50, BuyPrice = 20.00m, SellPrice = 24.00m, EmployeeNotes = "When one of the vilest creatures ever imprisoned in Hell is released back onto Earth, Spawn follows the clues right into a trap set just for him. But why does Kincaid want Spawn to ascend the throne of Hell, and what of the prophecy of the KING SPAWN?", ImageUrl = "/images/king_spawn_1.jpg" },
            new Product { ProductId = 25, ProdCatId = 3, Description = "KING SPAWN, VOL. 2 TP", Manufacturer = "Sean Lewis, Todd McFarlane", Stock = 50, BuyPrice = 20.00m, SellPrice = 24.00m, EmployeeNotes = "Spawn returns to where his journey began: New York City. This is where the God Throne, the Dead Zones, and the prophecy of King Spawn all collide. Will Spawn take the crown, or will he doom the world instead?", ImageUrl = "/images/king_spawn_2.jpg" },
            new Product { ProductId = 26, ProdCatId = 3, Description = "KING SPAWN, VOL. 3 TP", Manufacturer = "Sean Lewis, Todd McFarlane", Stock = 50, BuyPrice = 20.00m, SellPrice = 24.00m, EmployeeNotes = "All hail the KING! Spawn has assumed control over the Court of Priests, and former allies are beginning to doubt his loyalty to the mission. Unfortunately, like in any monarchy, plots are already in motion to depose the ruler. Spawn doesn't know who he can trust and who has him in their sights!", ImageUrl = "/images/king_spawn_3.jpg" },
            new Product { ProductId = 27, ProdCatId = 3, Description = "KING SPAWN, VOL. 4 TP", Manufacturer = "Sean Lewis, Todd McFarlane", Stock = 50, BuyPrice = 20.00m, SellPrice = 24.00m, EmployeeNotes = "Spawn realizes he's been a pawn in a dangerous game. With the DEADZONES open, a new enemy emerges, threatening to destroy the world. But Spawn won't back down, even as he faces a new force in his old stomping grounds. It's time to take back the city from the unsavory characters from the wrong side of Hell.", ImageUrl = "/images/king_spawn_4.jpg" },

            // Shonen Category Products
            new Product { ProductId = 28, ProdCatId = 4, Description = "Attack on Titan, Vol. 1", Manufacturer = "Hajime Isayama", Stock = 70, BuyPrice = 10.00m, SellPrice = 14.00m, EmployeeNotes = "For the past century, what's left of mankind has hidden in a giant, three-walled city, trapped in fear of the bizarre, giant humanoids known as the Titans. Little is known about where they came from or why they are bent on consuming humankind, but the sudden appearance of an enormous Titan is about to change everything...", ImageUrl = "/images/attack_on_titan_1.jpg" },
            new Product { ProductId = 29, ProdCatId = 4, Description = "Attack on Titan, Vol. 2", Manufacturer = "Hajime Isayama", Stock = 70, BuyPrice = 10.00m, SellPrice = 14.00m, EmployeeNotes = "The Colossal Titan has breached humanity’s first line of defense, Wall Maria. Mikasa, the 104th Training Corps’ ace and Eren’s best friend, may be the only one capable of defeating them, but beneath her calm exterior lurks a dark past. When all looks lost, a new Titan appears and begins to slaughter its fellow Titans. Could this new monster be a blessing in disguise, or is the truth something much more sinister?", ImageUrl = "/images/attack_on_titan_2.jpg" },
            new Product { ProductId = 30, ProdCatId = 4, Description = "Attack on Titan, Vol. 3", Manufacturer = "Hajime Isayama", Stock = 70, BuyPrice = 10.00m, SellPrice = 14.00m, EmployeeNotes = "The last thing Eren remembers before blacking out, a Titan had bitten off his arm and leg and was getting ready to eat him alive. Much to his surprise he wakes up without a scratch on him, with a crowd of angry soldiers screaming for his blood. What strange new power has he awakened, and what will happen when the boy devoted to destroying the Titans becomes one himself?", ImageUrl = "/images/attack_on_titan_3.jpg" },
            new Product { ProductId = 31, ProdCatId = 4, Description = "Chainsaw Man, Vol. 1: Dog and Chainsaw", Manufacturer = "Tatsuki Fujimoto", Stock = 70, BuyPrice = 10.00m, SellPrice = 14.00m, EmployeeNotes = "Denji’s a poor young man who’ll do anything for money, even hunting down devils with his pet devil-dog Pochita. He’s a simple man with simple dreams, drowning under a mountain of debt. But his sad life gets turned upside down one day when he’s betrayed by someone he trusts. Now with the power of a devil inside him, Denji’s become a whole new man—Chainsaw Man!", ImageUrl = "/images/chainsaw_man_1.jpg" },
            new Product { ProductId = 32, ProdCatId = 4, Description = "One Piece, Vol 1", Manufacturer = "Eiichiro Oda", Stock = 200, BuyPrice = 9.00m, SellPrice = 13.00m, EmployeeNotes = "Join Monkey D. Luffy and his swashbuckling crew in their search for the ultimate treasure, One Piece!\r\n\r\nAs a child, Monkey D. Luffy dreamed of becoming King of the Pirates. But his life changed when he accidentally gained the power to stretch like rubber…at the cost of never being able to swim again! Years, later, Luffy sets off in search of the “One Piece,” said to be the greatest treasure in the world...\r\n\r\nAs a child, Monkey D. Luffy was inspired to become a pirate by listening to the tales of the buccaneer \"Red-Haired\" Shanks. But his life changed when Luffy accidentally ate the Gum-Gum Devil Fruit and gained the power to stretch like rubber...at the cost of never being able to swim again! Years later, still vowing to become the king of the pirates, Luffy sets out on his adventure...one guy alone in a rowboat, in search of the legendary \"One Piece,\" said to be the greatest treasure in the world...", ImageUrl = "/images/one_piece_1.jpg" },
            new Product { ProductId = 33, ProdCatId = 4, Description = "One Piece, Vol 2", Manufacturer = "Eiichiro Oda", Stock = 200, BuyPrice = 9.00m, SellPrice = 13.00m, EmployeeNotes = "Join Monkey D. Luffy and his swashbuckling crew in their search for the ultimate treasure, One Piece!\r\n\r\nAs a child, Monkey D. Luffy dreamed of becoming King of the Pirates. But his life changed when he accidentally gained the power to stretch like rubber…at the cost of never being able to swim again! Years, later, Luffy sets off in search of the “One Piece,” said to be the greatest treasure in the world...\r\n\r\nAs a child, Monkey D. Luffy was inspired to become a pirate by listening to the tales of the buccaneer \"Red-Haired\" Shanks. But his life changed when Luffy accidentally ate the Gum-Gum Devil Fruit and gained the power to stretch like rubber...at the cost of never being able to swim again! Years later, still vowing to become the king of the pirates, Luffy sets out on his adventure...one guy alone in a rowboat, in search of the legendary \"One Piece,\" said to be the greatest treasure in the world...", ImageUrl = "/images/one_piece_2.jpg" },
            new Product { ProductId = 34, ProdCatId = 4, Description = "One Piece, Vol 3", Manufacturer = "Eiichiro Oda", Stock = 200, BuyPrice = 9.00m, SellPrice = 13.00m, EmployeeNotes = "Join Monkey D. Luffy and his swashbuckling crew in their search for the ultimate treasure, One Piece!\r\n\r\nAs a child, Monkey D. Luffy dreamed of becoming King of the Pirates. But his life changed when he accidentally gained the power to stretch like rubber…at the cost of never being able to swim again! Years, later, Luffy sets off in search of the “One Piece,” said to be the greatest treasure in the world...\r\n\r\nAs a child, Monkey D. Luffy was inspired to become a pirate by listening to the tales of the buccaneer \"Red-Haired\" Shanks. But his life changed when Luffy accidentally ate the Gum-Gum Devil Fruit and gained the power to stretch like rubber...at the cost of never being able to swim again! Years later, still vowing to become the king of the pirates, Luffy sets out on his adventure...one guy alone in a rowboat, in search of the legendary \"One Piece,\" said to be the greatest treasure in the world...", ImageUrl = "/images/one_piece_3.jpg" },
            new Product { ProductId = 35, ProdCatId = 4, Description = "One Piece, Vol 4", Manufacturer = "Eiichiro Oda", Stock = 200, BuyPrice = 9.00m, SellPrice = 13.00m, EmployeeNotes = "Join Monkey D. Luffy and his swashbuckling crew in their search for the ultimate treasure, One Piece!\r\n\r\nAs a child, Monkey D. Luffy dreamed of becoming King of the Pirates. But his life changed when he accidentally gained the power to stretch like rubber…at the cost of never being able to swim again! Years, later, Luffy sets off in search of the “One Piece,” said to be the greatest treasure in the world...\r\n\r\nAs a child, Monkey D. Luffy was inspired to become a pirate by listening to the tales of the buccaneer \"Red-Haired\" Shanks. But his life changed when Luffy accidentally ate the Gum-Gum Devil Fruit and gained the power to stretch like rubber...at the cost of never being able to swim again! Years later, still vowing to become the king of the pirates, Luffy sets out on his adventure...one guy alone in a rowboat, in search of the legendary \"One Piece,\" said to be the greatest treasure in the world...", ImageUrl = "/images/one_piece_5.jpg" },

            // Seinen Category Products
            new Product { ProductId = 36, ProdCatId = 5, Description = "Berserk, Vol 1", Manufacturer = "Kentaro Miura", Stock = 120, BuyPrice = 10.00m, SellPrice = 14.00m, EmployeeNotes = "Created by Kenturo Miura, Berserk is manga mayhem to the extreme - violent, horrifying, and mercilessly funny - and the wellspring for the internationally popular anime series. Not for the squeamish or the easily offended, Berserk asks for no quarter - and offers none!\r\nHis name is Guts, the Black Swordsman, a feared warrior spoken of only in whispers. Bearer of a gigantic sword, an iron hand, and the scars of countless battles and tortures, his flesh is also indelibly marked with The Brand, an unholy symbol that draws the forces of darkness to him and dooms him as their sacrifice. But Guts won't take his fate lying down; he'll cut a crimson swath of carnage through the ranks of the damned - and anyone else foolish enough to oppose him! Accompanied by Puck the Elf, more an annoyance than a companion, Guts relentlessly follows a dark, bloodstained path that leads only to death...or vengeance", ImageUrl = "/images/berserk_1.jpg" },
            new Product { ProductId = 37, ProdCatId = 5, Description = "Berserk, Vol 2", Manufacturer = "Kentaro Miura", Stock = 120, BuyPrice = 10.00m, SellPrice = 14.00m, EmployeeNotes = "Created by Kenturo Miura, Berserk is manga mayhem to the extreme - violent, horrifying, and mercilessly funny - and the wellspring for the internationally popular anime series. Not for the squeamish or the easily offended, Berserk asks for no quarter - and offers none!\r\nHis name is Guts, the Black Swordsman, a feared warrior spoken of only in whispers. Bearer of a gigantic sword, an iron hand, and the scars of countless battles and tortures, his flesh is also indelibly marked with The Brand, an unholy symbol that draws the forces of darkness to him and dooms him as their sacrifice. But Guts won't take his fate lying down; he'll cut a crimson swath of carnage through the ranks of the damned - and anyone else foolish enough to oppose him! Accompanied by Puck the Elf, more an annoyance than a companion, Guts relentlessly follows a dark, bloodstained path that leads only to death...or vengeance", ImageUrl = "/images/berserk_2.jpg" },
            new Product { ProductId = 38, ProdCatId = 5, Description = "Berserk, Vol 3", Manufacturer = "Kentaro Miura", Stock = 120, BuyPrice = 10.00m, SellPrice = 14.00m, EmployeeNotes = "Created by Kenturo Miura, Berserk is manga mayhem to the extreme - violent, horrifying, and mercilessly funny - and the wellspring for the internationally popular anime series. Not for the squeamish or the easily offended, Berserk asks for no quarter - and offers none!\r\nHis name is Guts, the Black Swordsman, a feared warrior spoken of only in whispers. Bearer of a gigantic sword, an iron hand, and the scars of countless battles and tortures, his flesh is also indelibly marked with The Brand, an unholy symbol that draws the forces of darkness to him and dooms him as their sacrifice. But Guts won't take his fate lying down; he'll cut a crimson swath of carnage through the ranks of the damned - and anyone else foolish enough to oppose him! Accompanied by Puck the Elf, more an annoyance than a companion, Guts relentlessly follows a dark, bloodstained path that leads only to death...or vengeance", ImageUrl = "/images/berserk_3.jpg" },
            new Product { ProductId = 39, ProdCatId = 5, Description = "Vinland Saga, Vol 1", Manufacturer = "Makoto Yukimura", Stock = 110, BuyPrice = 11.00m, SellPrice = 14.00m, EmployeeNotes = "As a child, Thorfinn sat at the feet of the great Leif Ericson and thrilled to wild tales of a land far to the west. But his youthful fantasies were shattered by a mercenary raid. Raised by the Vikings who murdered his family, Thorfinn became a terrifying warrior, forever seeking to kill the band's leader, Askeladd, and avenge his father. Sustaining Throfinn through his ordeal are his pride in his family and his dreams of a fertile westward land, a land without war or slavery...the land Leif called Vinland.", ImageUrl = "/images/vinland_saga_1.jpg" },
            new Product { ProductId = 40, ProdCatId = 5, Description = "Vinland Saga, Vol 2", Manufacturer = "Makoto Yukimura", Stock = 110, BuyPrice = 11.00m, SellPrice = 14.00m, EmployeeNotes = "The foolish King Ethelred has fled, and Askeladd's band is one of hundreds plundering the English countryside. Yet victory brings no peace to the elderly Danish King Sweyn, who worries that his untested, sensitive son Canute will never be ready to take the throne. The king's attempt to force his son to become a man places the young prince within the grasp of the gleeful killer Thorkell! Whoever holds Canute holds the key to the thrones of England and Denmark – and Askeladd has his own reasons for joining the fray!", ImageUrl = "/images/vinland_saga_2.jpg" },
            new Product { ProductId = 41, ProdCatId = 5, Description = "Vinland Saga, Vol 3", Manufacturer = "Makoto Yukimura", Stock = 110, BuyPrice = 11.00m, SellPrice = 14.00m, EmployeeNotes = "\r\n A BLOODY COMING OF AGEIn a gambit to become the power behind the Danish and English thrones, Askeladd has taken the prince, Canute, and plunged deep into a winter storm behind enemy lines. Canute's father, King Sweyn, gives him up for dead in his haste to suppress English resistance. But Askeladd's small band can't outrun the tenacious maniac Thorkell forever, and when the warriors finally clash, a storm of sweat and gore ensues that will turn a boy into a man and a hostage into a ruler of men!", ImageUrl = "/images/vinland_saga_3.jpg" }
        );

        // Seed default global settings
        modelBuilder.Entity<GlobalSettings>().HasData(
            new GlobalSettings { Id = 1, MinStockLimit = 10, MaxStockLimit = 300 }
        );
        modelBuilder.Entity<Customer>().HasData(
            new Customer() { CustomerId = 1, FirstName = "Sebastian", LastName = "Burke", Email = "sebastian@ipf-mail.com", PhoneNumber = "81923020002", Province = "QC", CreditCard = "1234123412341234" },
            new Customer() { CustomerId = 2, FirstName = "Richard", LastName = "Chan", Email = "rchan@email.com", PhoneNumber = "1234567890", Province = "QC", CreditCard = "1234123412341234" },
            new Customer() { CustomerId = 3, FirstName = "Zach", LastName = "Fortier", Email = "zach@email.com", PhoneNumber = "1234567890", Province = "QC", CreditCard = "1234123412341234" }
            );
        modelBuilder.Entity<ShoppingCart>().HasData(
            new ShoppingCart() { ShoppingCartId = 1, CustomerId = 1, DateCreated = new DateTime(2023, 9, 16) }
            );
        modelBuilder.Entity<CartItem>().HasData(
            new CartItem() { CartItemId = 1, CartId = 1, ProductId = 1, Quantity = 1, Price = 50 },
            new CartItem() { CartItemId = 2, CartId = 1, ProductId = 9, Quantity = 2, Price = 34 }
            );
        modelBuilder.Entity<Order>().HasData(
            new Order() { OrderId = 1, CustomerId = 2, DateCreated = new DateTime(2023, 9, 16), DateFufilled = new DateTime(2023, 9, 21), Total = 423, Taxes = 282 }
            );
        modelBuilder.Entity<OrderItem>().HasData(
            new OrderItem() { OrderItemId = 1, OrderId = 1, ProductId = 1, Quantity = 1, Price = 25 },
            new OrderItem() { OrderItemId = 2, OrderId = 1, ProductId = 10, Quantity = 2, Price = 250 },
            new OrderItem() { OrderItemId = 3, OrderId = 1, ProductId = 3, Quantity = 1, Price = 13 }
            );
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
