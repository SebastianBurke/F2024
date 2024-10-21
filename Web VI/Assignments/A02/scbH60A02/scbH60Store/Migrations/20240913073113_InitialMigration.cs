﻿ using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace scbH60Store.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlobalSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinStockLimit = table.Column<int>(type: "int", nullable: false),
                    MaxStockLimit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdCat = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdCatId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: true),
                    Manufacturer = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    BuyPrice = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    SellPrice = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    EmployeeNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Product_ProductCategory",
                        column: x => x.ProdCatId,
                        principalTable: "ProductCategory",
                        principalColumn: "CategoryID");
                });

            migrationBuilder.InsertData(
                table: "GlobalSettings",
                columns: new[] { "Id", "MaxStockLimit", "MinStockLimit" },
                values: new object[] { 1, 300, 10 });

            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "CategoryID", "ImageUrl", "ProdCat" },
                values: new object[,]
                {
                    { 1, "/images/dc_comics.jpg", "DC Comics" },
                    { 2, "/images/marvel_comics.jpg", "Marvel" },
                    { 3, "/images/image_comics.jpg", "Image Comics" },
                    { 4, "/images/shonen_manga.jpg", "Shonen" },
                    { 5, "/images/seinen_manga.jpg", "Seinen" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductID", "BuyPrice", "Description", "EmployeeNotes", "ImageUrl", "Manufacturer", "ProdCatId", "SellPrice", "Stock" },
                values: new object[,]
                {
                    { 1, 10.00m, "All-Star Batman: The Deluxe Edition HC", "Acclaimed writer Scott Snyder joins forces with legendary artists including John Romita Jr. and BATMAN: THE BLACK MIRROR collaborator, Jock, to bring fans a Batman tale like no other. Snyder and team dive into Batman’s complicated relationships with some of the greatest villains of popular culture, with a modern sensibility.\r\n\r\nWhat does a road trip with disgraced district attorney Harvey Dent, now the villainous Two-Face, look like? Then dive into chilling tales featuring Mr. Freeze and Poison Ivy.  Finally, the mastermind behind these villains and there twisted end goal will be revealed!", "/images/all_star_batman_the_deluxe_edition.jpg", "Scott Snyder", 1, 25.00m, 100 },
                    { 2, 8.00m, "Wesley Dodds: The Sandman TP", "Wesley Dodds' dream of a better world is now a nightmare, as DC’s original Sandman returns in a gripping new noir mystery!\r\n\r\nNo one escapes the Sandman's dark dreams, not even Wesley Dodds himself. After years of testing and experimentation, Wesley perfected his sleep gas as the optimal weapon to fight crime without causing undue harm. But when his journal detailing all his failed and far more deadly formulas is stolen, the Sandman must hunt down the thief and the people in the shadows pulling the strings!\r\n\r\nCan Wesley solve the mystery of who broke into his home before these noxious weapons are unleashed on the world, or is Sandman fated to fade away into the mists?\r\n\r\nWesley Dodds: The Sandman is written by comics superstar Robert Venditti (Superman ’78) and vividly drawn by fan-favorite artist Riley Rossmo (Harley Quinn). A bold and thoroughly modern exploration of one of comics’ most classic characters, Wesley Dodds: The Sandman is part of DC’s The New Golden Age initiative, along with Jay Garrick: The Flash and Alan Scott: The Green Lantern.", "/images/the_sandman.jpg", "Robert Venditti, Riley Rossmo, Ivan Plascencia, Tom Napolitano", 1, 17.00m, 140 },
                    { 3, 9.00m, "Nightwing: Uncovered #1", "Dick Grayson is front and center in a stunning collection of some of the most compelling cover art to grace his solo title over the years!Dick Grayson is front and center in a stunning collection of some of the most compelling cover art to grace his solo title over the years!", "/images/nightwing_uncovered_1.jpg", "Ivan Cohen, Dexter Soy", 1, 13.00m, 70 },
                    { 4, 12.00m, "Batman: The Long Halloween - The Last Halloween #0", "A NEW EDITION OF THE FINAL COLLABORATION BETWEEN JEPH LOEB AND TIM SALE! Prelude to Batman The Long Halloween: The Last Halloween! You thought you knew the whole story of Batman: The Long Halloween... you were wrong! Reprinting the final collaboration between legendary creators Jeph Loeb and Tim Sale, this special uncovers a deadly mystery that could destroy Batman, Commissioner Gordon, Two-Face, and... well, that would be telling, wouldn't it? Don't miss out on this special reprint of the prelude to THE LAST HALLOWEEN.", "/images/batman_the_long_halloween_the_last_halloween_0.jpg", "Jeph Loeb, Tim Sale, Brennan Wagner, Richard Starkings, Ben Abernathy", 1, 18.00m, 80 },
                    { 5, 12.00m, "Superman #18", "ABSOLUTE POWER TIE-IN! Waller has the powerless heroes of the DC Universe on the ropes! Can the powerless Superman and Zatanna find the mystical map to the Dark Roads in time to get some major back-up?! Lex Luthor, Lois Lane, Mercy, Jimmy, and Silver Banshee are on the run from the superpowered Amazos but find themselves pulled into a battle for the soul of Metropolis! Don't miss the shocking cliffhanger that impacts the future of the Superman titles!", "/images/superman_18.jpg", "Joshua Williamson, Jamal Campbell", 1, 18.00m, 80 },
                    { 6, 12.00m, "Batman Day 2024: Batman / Elmer Fudd Special Noir #1", "", "/images/batman_day_2024_batman_elmer_fudd_special_noir_1.jpg", "Tom King, Lee Weeks, Deron Bennett", 1, 18.00m, 80 },
                    { 7, 12.00m, "Azrael #20", "The Orchid brothers find a way to end their blood feud: destroy Azrael! Trapped on an island with the murderous siblings, Jean-Paul Valley is shocked to find that, at the most dire moment, his Azrael persona deserts him!", "/images/azrael_20.jpg", "Dennis O'Neil, Tom Grindberg, James Pascoe, Ken Bruzenak, Chuck Kim", 1, 18.00m, 80 },
                    { 8, 12.00m, "American Vampire Book One: DC Compact Comics Edition TP", "Chronicling the history of a new breed of vampire, AMERICAN VAMPIRE by the legendary Scott Snyder and Stephen King is a fresh look at an old monster — a generational epic showcasing the bloodlust that lay hidden beneath America's most distinctive eras. Cunning, ruthless, and rattlesnake mean, Skinner Sweet is a thoroughly corrupt gunslinger. When European vampires come to the American Old West, they turn Skinner into a true monster: the very first American vampire.\r\n\r\nSkinner becomes something entirely new — a stronger breed of vampire immune to sunlight, who hates every last one of his aristocratic European ancestors.\r\n\r\nFollow this dark symbol of the New World's bloody path as he moves through American history's most distinctive eras — from the Wild West in the 1880s to the glamorous classic Hollywood of the 1920s to mobster-run Las Vegas in the 1930s, and beyond.\r\n\r\nBut as Skinner's war with his predecessors inspires a mysterious society to rise and fight them both, his most upsetting decision might involve the first person he chooses to join his vampiric ranks: a struggling young movie star named Pearl Jones.", "/images/american_vampire_1.jpg", "Scott Snyder, Rafael Albuquerque, Stephen King, Dave McCaig, Steve Wands", 1, 18.00m, 80 },
                    { 9, 5.00m, "Wolverine (2024) #1", "THE LEGEND BEGINS ANEW IN THE ADAMANTIUM-TOUGH NEW ONGOING SERIES! There's a killer in the woods - and as WOLVERINE's attempt at piece is shattered, an OLD ENEMY will re-emerge as a NEW VILLAIN rises who will bring LOGAN to the brink of his berserker rage. But NIGHTCRAWLER knows his old friend is capable of doing what's right, and before long, Logan will have to unleash his claws, push his healing factor to the limit and demonstrate he's the best there is at what he does once and for all - nice be damned! The legendary WOLVERINE ongoing series kicks off anew with the superstar creative team of Saladin Ahmed (DAREDEVIL, MS. MARVEL) and Martín Cóccolo (DEADPOOL, IMMORTAL THOR) beginning their epic journey with Logan! Collector's Note: A key FIRST APPEARANCE and a major addition to the lore of Wolverine in this issue!", "/images/Wolverine_2024_1.jpg", "Saladin Ahmed, Martin Coccolo", 2, 14.00m, 120 },
                    { 10, 5.00m, "Wolverine (2024) #2", "WHERE GOES THE WENDIGO?! Who stalks WOLVERINE in the Canadian North? And what mysterious designs does the WENDIGO have on the Best There Is? Logan just wants to be left alone, but a war on two fronts will evolve with an unexpected turn! Don't miss the debut of the all-new Wendigo, as the secret it hides will shape Wolverine's mission…", "/images/Wolverine_2024_2.jpg", "Saladin Ahmed, Martin Coccolo", 2, 14.00m, 120 },
                    { 11, 5.00m, "Wolverine (2024) #3", "DEPARTMENT H GOES HUNTING! Canada's DEPARTMENT H has their sights trained once more on WOLVERINE! Years ago, they played a role in WEAPON X and LOGAN's first assignment, but what else are they hunting now that mutants are hated and feared more than ever? Meanwhile, Wolverine's UNLIKELY ALLY may have just killed an innocent…and OLD ENEMIES of Wolverine's gather as more sinister machinations unfurl… A key issue, as the ALL-NEW villain moving against Wolverine comes into sharper focus…", "/images/Wolverine_2024_3.jpg", "Saladin Ahmed, Martin Coccolo", 2, 14.00m, 120 },
                    { 12, 6.00m, "Spider-Man: Reign 2 (2024) #1", "BACK TO THE (AMAZING SPIDER-MAN'S) FUTURE! Award-winning writer/artist Kaare Andrews returns to the world of SPIDER-MAN'S dystopian future in this sequel to the landmark, genre-defying SPIDER-MAN: REIGN! And who is the new BLACK CAT?! What tragedies and triumphs await this older, grizzled Peter Parker? Peter isn't the only one who aged… wait until you see what happened to MILES MORALES!", "/images/spider_man_reign_2_2024_1.jpg", "Kaare Andrews", 2, 15.00m, 130 },
                    { 13, 6.00m, "Spider-Man: Reign 2 (2024) #2", "Old Man Peter returns to the past! Can he save the future and, more importantly, Mary Jane? Not if MILES MORALES has anything to say about it. You don't want to miss the latest chapter of the most notorious Spidey story ever told!", "/images/spider_man_reign_2_2024_2.jpg", "Kaare Andrews", 2, 15.00m, 130 },
                    { 14, 6.00m, "Spider-Man: Reign 2 (2024) #3", "Old Man Peter Parker is lashing out and making wildly bad decisions, but what else is new? Well, now he's got Miles Morales after him (and Miles is no spring chicken himself). The Spider-War is fought, and the whole of existence may very well be at stake as time and space get pulled to the brink!", "/images/spider_man_reign_2_2024_3.jpg", "Kaare Andrews", 2, 15.00m, 130 },
                    { 15, 4.00m, "Vision (2015) #1", "The Vision wants to be human, and what's more human than family? He goes to the laboratory where he was created, where Ultron molded him into a weapon, where he first rebelled against his given destiny, where he first imagined that he could be more, that he could be good, that he could be a man, a normal, ordinary man. And he builds them. A wife, Virginia. Two teenage twins, Viv and Vin. They look like him. They have his powers. They share his grandest ambition or perhaps obsession: the unrelenting need to be ordinary. Behold The Visions! They're the family next door, and they have the power to kill us all. What could possibly go wrong?", "/images/vision_2015_1.jpg", "Tom King, Gabriel Hernandez Walta, Mike Del Mundo", 2, 12.00m, 60 },
                    { 16, 4.00m, "Vision (2015) #2", "After the stunning, heart-stopping events of Vision #1, the Avengers will never be the same. The Vision and his family attempt to cope with these events in their own, unique way. Though they put on a happy face, each of them can feel their anger growing, blistering, tearing them apart. Will the Visions be able to hold together, or will that anger destroy them and the world around them?", "/images/vision_2015_2.jpg", "Tom King, Gabriel Hernandez Walta, Mike Del Mundo", 2, 12.00m, 60 },
                    { 17, 4.00m, "Vision (2015) #3", "A house attacked. A daughter dying. An old, dead friend screaming out in pain. This wasn't how it was supposed to go. The Vision created his family to be normal. This isn't normal. This is terrifying. And it's just the beginning. The epic tale of Vision and his family continues as he fights to remain ordinary, and that fight starts to tear his ordinary world apart.", "/images/vision_2015_3.jpg", "Tom King, Gabriel Hernandez Walta, Mike Del Mundo", 2, 12.00m, 60 },
                    { 18, 12.00m, "Deadpool #6", "Wade Wilson triumphed against Death Grip! This is the first issue of a new arc and killing Deadpool NOW would be an INSANE thing to do. Which is exactly why we’re doing it.", "/images/deadpool_6.jpg", "Cody Ziglar, Rogê Antônio", 2, 18.00m, 80 },
                    { 19, 15.00m, "Invincible, Vol. 1 (TBP)", "Mark Grayson is just like most everyone else his age. Except his father is the most powerful superhero on the planet-Omni-Man. When Mark develops powers of his own it's a dream come true. But living up to his father's legacy is only the beginning of Mark's problems...", "/images/invincible_1.jpg", "Robert Kirkman, Bill Crabtree, Cory Walker", 3, 30.00m, 160 },
                    { 20, 12.00m, "Invincible #144", "\"THE END OF ALL THINGS,\" Conclusion\r\n\r\nFinal issue.\r\n\r\nEverything since issue one has been building to this. Nothing can prepare you.", "/images/invincible_144.jpg", "Robert Kirkman, Nathan Fairbairn, Cory Walker", 3, 20.00m, 120 },
                    { 21, 12.00m, "Geiger (2024) #1", "SERIES PREMIERE\r\nIT ALL STARTS HERE! The critically acclaimed team of storytellers GEOFF JOHNS and GARY FRANK (GEIGER: GROUND ZERO, Doomsday Clock) return to the nuclear wasteland of their bestselling GEIGER for an ALL-NEW ONGOING series starring the violent and unpredictable GLOWING MAN!\r\nLeaving his home behind, Tariq Geiger now walks the radioactive roads of the former United States with his two-headed wolf Barney. But as his enemies doggedly pursue him, Geiger discovers salvation from the unlikeliest of foes. But what secrets does this potential ally hold that could help Geiger? And exactly how many people are after The Glowing Man… and why?\r\nDon’t miss this vital, action-packed chapter in the shared universe of THE UNNAMED saga and the momentous Ghost Machine rollout!", "/images/geiger_1.jpg", "Geoff Johns, Gary Frank, Brand Anderson", 3, 20.00m, 120 },
                    { 22, 18.00m, "Geiger (2024) #3", "Tariq Geiger surrounds himself with some dangerous friends. His two-headed wolf Barney bears the trauma of the fateful night that Geiger found him. And he and Geiger’s surprising new companion try to atone for a life of unfettered violence and brutality. But even between the three of them, they are no match for the many threats in pursuit. Plus, the return of Junkyard Joe!", "/images/geiger_3.jpg", "Geoff Johns, Gary Frank, Brand Anderson", 3, 20.00m, 80 },
                    { 23, 16.00m, "Geiger (2024) #5", "This is it! The first four issues of this new series have all pointed toward this final battle between Geiger and The Electrician! But it might be a quick one: The Electrician’s clever trap hits Geiger's greatest weakness, and his friends are helpless to halt it. Also: the final fate of Barney, the two-headed mutant wolf!", "/images/geiger_5.jpg", "Geoff Johns, Gary Frank, Brand Anderson", 3, 20.00m, 90 },
                    { 24, 20.00m, "KING SPAWN, VOL. 1 TP", "When one of the vilest creatures ever imprisoned in Hell is released back onto Earth, Spawn follows the clues right into a trap set just for him. But why does Kincaid want Spawn to ascend the throne of Hell, and what of the prophecy of the KING SPAWN?", "/images/king_spawn_1.jpg", "Sean Lewis, Todd McFarlane", 3, 24.00m, 50 },
                    { 25, 20.00m, "KING SPAWN, VOL. 2 TP", "Spawn returns to where his journey began: New York City. This is where the God Throne, the Dead Zones, and the prophecy of King Spawn all collide. Will Spawn take the crown, or will he doom the world instead?", "/images/king_spawn_2.jpg", "Sean Lewis, Todd McFarlane", 3, 24.00m, 50 },
                    { 26, 20.00m, "KING SPAWN, VOL. 3 TP", "All hail the KING! Spawn has assumed control over the Court of Priests, and former allies are beginning to doubt his loyalty to the mission. Unfortunately, like in any monarchy, plots are already in motion to depose the ruler. Spawn doesn't know who he can trust and who has him in their sights!", "/images/king_spawn_3.jpg", "Sean Lewis, Todd McFarlane", 3, 24.00m, 50 },
                    { 27, 20.00m, "KING SPAWN, VOL. 4 TP", "Spawn realizes he's been a pawn in a dangerous game. With the DEADZONES open, a new enemy emerges, threatening to destroy the world. But Spawn won't back down, even as he faces a new force in his old stomping grounds. It's time to take back the city from the unsavory characters from the wrong side of Hell.", "/images/king_spawn_4.jpg", "Sean Lewis, Todd McFarlane", 3, 24.00m, 50 },
                    { 28, 10.00m, "Attack on Titan, Vol. 1", "For the past century, what's left of mankind has hidden in a giant, three-walled city, trapped in fear of the bizarre, giant humanoids known as the Titans. Little is known about where they came from or why they are bent on consuming humankind, but the sudden appearance of an enormous Titan is about to change everything...", "/images/attack_on_titan_1.jpg", "Hajime Isayama", 4, 14.00m, 70 },
                    { 29, 10.00m, "Attack on Titan, Vol. 2", "The Colossal Titan has breached humanity’s first line of defense, Wall Maria. Mikasa, the 104th Training Corps’ ace and Eren’s best friend, may be the only one capable of defeating them, but beneath her calm exterior lurks a dark past. When all looks lost, a new Titan appears and begins to slaughter its fellow Titans. Could this new monster be a blessing in disguise, or is the truth something much more sinister?", "/images/attack_on_titan_2.jpg", "Hajime Isayama", 4, 14.00m, 70 },
                    { 30, 10.00m, "Attack on Titan, Vol. 3", "The last thing Eren remembers before blacking out, a Titan had bitten off his arm and leg and was getting ready to eat him alive. Much to his surprise he wakes up without a scratch on him, with a crowd of angry soldiers screaming for his blood. What strange new power has he awakened, and what will happen when the boy devoted to destroying the Titans becomes one himself?", "/images/attack_on_titan_3.jpg", "Hajime Isayama", 4, 14.00m, 70 },
                    { 31, 10.00m, "Chainsaw Man, Vol. 1: Dog and Chainsaw", "Denji’s a poor young man who’ll do anything for money, even hunting down devils with his pet devil-dog Pochita. He’s a simple man with simple dreams, drowning under a mountain of debt. But his sad life gets turned upside down one day when he’s betrayed by someone he trusts. Now with the power of a devil inside him, Denji’s become a whole new man—Chainsaw Man!", "/images/chainsaw_man_1.jpg", "Tatsuki Fujimoto", 4, 14.00m, 70 },
                    { 32, 9.00m, "One Piece, Vol 1", "Join Monkey D. Luffy and his swashbuckling crew in their search for the ultimate treasure, One Piece!\r\n\r\nAs a child, Monkey D. Luffy dreamed of becoming King of the Pirates. But his life changed when he accidentally gained the power to stretch like rubber…at the cost of never being able to swim again! Years, later, Luffy sets off in search of the “One Piece,” said to be the greatest treasure in the world...\r\n\r\nAs a child, Monkey D. Luffy was inspired to become a pirate by listening to the tales of the buccaneer \"Red-Haired\" Shanks. But his life changed when Luffy accidentally ate the Gum-Gum Devil Fruit and gained the power to stretch like rubber...at the cost of never being able to swim again! Years later, still vowing to become the king of the pirates, Luffy sets out on his adventure...one guy alone in a rowboat, in search of the legendary \"One Piece,\" said to be the greatest treasure in the world...", "/images/one_piece_1.jpg", "Eiichiro Oda", 4, 13.00m, 200 },
                    { 33, 9.00m, "One Piece, Vol 2", "Join Monkey D. Luffy and his swashbuckling crew in their search for the ultimate treasure, One Piece!\r\n\r\nAs a child, Monkey D. Luffy dreamed of becoming King of the Pirates. But his life changed when he accidentally gained the power to stretch like rubber…at the cost of never being able to swim again! Years, later, Luffy sets off in search of the “One Piece,” said to be the greatest treasure in the world...\r\n\r\nAs a child, Monkey D. Luffy was inspired to become a pirate by listening to the tales of the buccaneer \"Red-Haired\" Shanks. But his life changed when Luffy accidentally ate the Gum-Gum Devil Fruit and gained the power to stretch like rubber...at the cost of never being able to swim again! Years later, still vowing to become the king of the pirates, Luffy sets out on his adventure...one guy alone in a rowboat, in search of the legendary \"One Piece,\" said to be the greatest treasure in the world...", "/images/one_piece_2.jpg", "Eiichiro Oda", 4, 13.00m, 200 },
                    { 34, 9.00m, "One Piece, Vol 3", "Join Monkey D. Luffy and his swashbuckling crew in their search for the ultimate treasure, One Piece!\r\n\r\nAs a child, Monkey D. Luffy dreamed of becoming King of the Pirates. But his life changed when he accidentally gained the power to stretch like rubber…at the cost of never being able to swim again! Years, later, Luffy sets off in search of the “One Piece,” said to be the greatest treasure in the world...\r\n\r\nAs a child, Monkey D. Luffy was inspired to become a pirate by listening to the tales of the buccaneer \"Red-Haired\" Shanks. But his life changed when Luffy accidentally ate the Gum-Gum Devil Fruit and gained the power to stretch like rubber...at the cost of never being able to swim again! Years later, still vowing to become the king of the pirates, Luffy sets out on his adventure...one guy alone in a rowboat, in search of the legendary \"One Piece,\" said to be the greatest treasure in the world...", "/images/one_piece_3.jpg", "Eiichiro Oda", 4, 13.00m, 200 },
                    { 35, 9.00m, "One Piece, Vol 4", "Join Monkey D. Luffy and his swashbuckling crew in their search for the ultimate treasure, One Piece!\r\n\r\nAs a child, Monkey D. Luffy dreamed of becoming King of the Pirates. But his life changed when he accidentally gained the power to stretch like rubber…at the cost of never being able to swim again! Years, later, Luffy sets off in search of the “One Piece,” said to be the greatest treasure in the world...\r\n\r\nAs a child, Monkey D. Luffy was inspired to become a pirate by listening to the tales of the buccaneer \"Red-Haired\" Shanks. But his life changed when Luffy accidentally ate the Gum-Gum Devil Fruit and gained the power to stretch like rubber...at the cost of never being able to swim again! Years later, still vowing to become the king of the pirates, Luffy sets out on his adventure...one guy alone in a rowboat, in search of the legendary \"One Piece,\" said to be the greatest treasure in the world...", "/images/one_piece_5.jpg", "Eiichiro Oda", 4, 13.00m, 200 },
                    { 36, 10.00m, "Berserk, Vol 1", "Created by Kenturo Miura, Berserk is manga mayhem to the extreme - violent, horrifying, and mercilessly funny - and the wellspring for the internationally popular anime series. Not for the squeamish or the easily offended, Berserk asks for no quarter - and offers none!\r\nHis name is Guts, the Black Swordsman, a feared warrior spoken of only in whispers. Bearer of a gigantic sword, an iron hand, and the scars of countless battles and tortures, his flesh is also indelibly marked with The Brand, an unholy symbol that draws the forces of darkness to him and dooms him as their sacrifice. But Guts won't take his fate lying down; he'll cut a crimson swath of carnage through the ranks of the damned - and anyone else foolish enough to oppose him! Accompanied by Puck the Elf, more an annoyance than a companion, Guts relentlessly follows a dark, bloodstained path that leads only to death...or vengeance", "/images/berserk_1.jpg", "Kentaro Miura", 5, 14.00m, 120 },
                    { 37, 10.00m, "Berserk, Vol 2", "Created by Kenturo Miura, Berserk is manga mayhem to the extreme - violent, horrifying, and mercilessly funny - and the wellspring for the internationally popular anime series. Not for the squeamish or the easily offended, Berserk asks for no quarter - and offers none!\r\nHis name is Guts, the Black Swordsman, a feared warrior spoken of only in whispers. Bearer of a gigantic sword, an iron hand, and the scars of countless battles and tortures, his flesh is also indelibly marked with The Brand, an unholy symbol that draws the forces of darkness to him and dooms him as their sacrifice. But Guts won't take his fate lying down; he'll cut a crimson swath of carnage through the ranks of the damned - and anyone else foolish enough to oppose him! Accompanied by Puck the Elf, more an annoyance than a companion, Guts relentlessly follows a dark, bloodstained path that leads only to death...or vengeance", "/images/berserk_2.jpg", "Kentaro Miura", 5, 14.00m, 120 },
                    { 38, 10.00m, "Berserk, Vol 3", "Created by Kenturo Miura, Berserk is manga mayhem to the extreme - violent, horrifying, and mercilessly funny - and the wellspring for the internationally popular anime series. Not for the squeamish or the easily offended, Berserk asks for no quarter - and offers none!\r\nHis name is Guts, the Black Swordsman, a feared warrior spoken of only in whispers. Bearer of a gigantic sword, an iron hand, and the scars of countless battles and tortures, his flesh is also indelibly marked with The Brand, an unholy symbol that draws the forces of darkness to him and dooms him as their sacrifice. But Guts won't take his fate lying down; he'll cut a crimson swath of carnage through the ranks of the damned - and anyone else foolish enough to oppose him! Accompanied by Puck the Elf, more an annoyance than a companion, Guts relentlessly follows a dark, bloodstained path that leads only to death...or vengeance", "/images/berserk_3.jpg", "Kentaro Miura", 5, 14.00m, 120 },
                    { 39, 11.00m, "Vinland Saga, Vol 1", "As a child, Thorfinn sat at the feet of the great Leif Ericson and thrilled to wild tales of a land far to the west. But his youthful fantasies were shattered by a mercenary raid. Raised by the Vikings who murdered his family, Thorfinn became a terrifying warrior, forever seeking to kill the band's leader, Askeladd, and avenge his father. Sustaining Throfinn through his ordeal are his pride in his family and his dreams of a fertile westward land, a land without war or slavery...the land Leif called Vinland.", "/images/vinland_saga_1.jpg", "Makoto Yukimura", 5, 14.00m, 110 },
                    { 40, 11.00m, "Vinland Saga, Vol 2", "The foolish King Ethelred has fled, and Askeladd's band is one of hundreds plundering the English countryside. Yet victory brings no peace to the elderly Danish King Sweyn, who worries that his untested, sensitive son Canute will never be ready to take the throne. The king's attempt to force his son to become a man places the young prince within the grasp of the gleeful killer Thorkell! Whoever holds Canute holds the key to the thrones of England and Denmark – and Askeladd has his own reasons for joining the fray!", "/images/vinland_saga_2.jpg", "Makoto Yukimura", 5, 14.00m, 110 },
                    { 41, 11.00m, "Vinland Saga, Vol 3", "\r\n A BLOODY COMING OF AGEIn a gambit to become the power behind the Danish and English thrones, Askeladd has taken the prince, Canute, and plunged deep into a winter storm behind enemy lines. Canute's father, King Sweyn, gives him up for dead in his haste to suppress English resistance. But Askeladd's small band can't outrun the tenacious maniac Thorkell forever, and when the warriors finally clash, a storm of sweat and gore ensues that will turn a boy into a man and a hostage into a ruler of men!", "/images/vinland_saga_3.jpg", "Makoto Yukimura", 5, 14.00m, 110 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProdCatId",
                table: "Product",
                column: "ProdCatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalSettings");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProductCategory");
        }
    }
}
