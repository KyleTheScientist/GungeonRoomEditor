using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlaceableDatabase : TileDatabase
{
    public override Dictionary<string, string> Entries { get; set; } = new Dictionary<string, string>()
    {
        //TABLES
        { "adaptive_table_horizontal", "horizontal table" },
        { "adaptive_table_vertical", "vertical table" },
        { "table_horizontal", "Table_Horizontal" },
        { "table_vertical", "Table_Vertical" },
        { "stone_table_horizontal", "Table_Horizontal_Stone" },
        { "stone_table_vertical", "Table_Vertical_Stone" },
        { "coffin_vertical", "Coffin_Vertical" },
        { "coffin_horizontal", "Coffin_Horizontal" },

        //BARRELS
        { "random_drum", "drum_variety" },
        { "poison_drum", "Yellow Drum" },
        { "explosive_drum", "Red Drum" },
        { "water_drum", "Blue Drum" },
        { "oil_drum", "Purple Drum"},
        { "explosive_barrel", "Red Barrel" },
        { "ice_cube_bomb", "Ice Cube Bomb" },

        //NPCS
        { "old_red", "Merchant_Blank" },
        { "cursula", "Merchant_Curse" },
        { "flynt", "Merchant_Key" },
        { "trorc", "Merchant_Truck" },
        { "professor_goopton", "NPC_Goop_Merchant" },
        { "vampire", "NPC_Vampire" },
        { "sell_creep", "SellPit" },
        { "old_man", "NPC_Old_Man" },
        { "synergrace", "NPC_Synergrace" },
        { "muncher", "NPC_GunberMuncher" },
        { "evil_muncher", "NPC_GunberMuncher_Evil" },
        { "monster_manuel", "NPC_Monster_Manuel" },
        { "brother_albern", "NPC_Truth_Knower" },
        { "patches_and_mendy", "NPC_Smash_Tent" },
        { "witches", "NPC_Witches" },

        //Shrines
        { "ammo_shrine", "Shrine_Ammo" },
        { "beholster_shrine", "Shrine_Beholster" },
        { "blank_shrine", "Shrine_Blank" },
        { "blood_shrine", "Shrine_Blood" },
        { "cleanse_shrine", "Shrine_Cleanse" },
        { "companion_shrine", "Shrine_Companion" },
        { "dice_shrine", "Shrine_Dice" },
        { "angel_shrine", "Shrine_FallenAngel" },
        { "glass_shrine", "Shrine_Glass" },
        { "health_shrine", "Shrine_Health" },
        { "hero_shrine", "Shrine_Hero" },
        { "junk_shrine", "Shrine_Junk" },
        { "cursed_mirror", "Shrine_Mirror" },
        { "yv_shrine", "Shrine_YV" },
        //Lunk and Gunsling King are broken

        //TRAPS
        { "curse_pot", "curse pot" },
        { "floor_spikes", "trap_spike_gungeon_2x2" },
        { "flame_trap", "trap_flame_poofy_gungeon_1x1" },
        { "pitfall_trap", "trap_pit_gungeon_trigger_2x2" },
        { "forge_shoot_face_west", "forge_face_shootseast" },
        { "forge_shoot_face_north", "forge_face_shootssouth" },
        { "forge_shoot_face_east", "forge_face_shootswest" },
        //{ "moving_platform_3x3", "default_platform_3x3" },
        
        //Won't work without additional setup:
        /* Logs
         * Sawblades
         * Mine Collapses
         * Crush Traps
         * default_platform_3x3 (moves)
         */
        //MISSING TRAPS: Flaming Pipes, Rotating Fire Bars, Fire Rings, Crush Traps

        //Chests
        { "double_loot_brown_chest", "Chest_Wood_Two_Items" },
        { "truth_chest", "TruthChest" },
        { "black_chest", "chest_black"},
        { "green_chest", "chest_green"},
        { "rainbow_chest", "chest_rainbow"},
        { "rat_chest", "chest_rat"},
        { "red_chest", "chest_red"},
        { "blue_chest", "chest_silver"},
        { "synergy_chest", "chest_synergy"},
        { "random_chest", "aaa_floorchestplacer"},
        { "random_mimic_chest", "almostdefinitelyamimicchestplacer"},

        //ITEMS
        { "key", "keybullet placeable" },
        { "random_pickup", "secret_room_pickup_placeable_garaunteed" },
        { "blank", "blank" },		// (A humble blank)
        { "ammo", "ammo_pickup" },
        { "spread_ammo", "ammo_pickup_spread" },

        //MISC
        { "heart_dispenser", "HeartDispenser" },
        { "teleporter", "teleporter" },
        { "elevator_arrival", "elevator_arrival" },
        { "elevator_departure", "elevator_departure" },

        //DECOR
        { "chest_platform", "gungeon_treasure_dais" },
        { "chest_platform_carpet", "treasure_dais_stone_carpet" },
        { "chandelier", "Hanging_Chandelier" },
        { "hanging_pot", "Hanging_Pot" },
        { "bench_vertical", "monk_bench_tall" },
        { "random_statue", "rat figure random" },
        { "barrel", "barrel_collection"},
        { "crate", "crate"},
        { "globe", "Table_Globe" },
        { "stack_of_books", "pileorstackofbooks"},
        { "pot", "pot"},
        { "tall_pot", "Tall Pot"},
        { "blue_pot", "Default Pot Blue" },
        { "rock", "rock"},
        { "bush", "bush"},
        { "bush_flowers", "bush flowers"},
        { "apple_barrel", "barrel_apple"},
        { "suit_of_armour", "suitofarmor"},
        { "ash_bulletman", "ash_bulletman"},
        { "ash_shotgun_man", "ash_shotgunman"},
        { "green_lantern", "Shrine_Lantern" },
        { "purple_lantern", "Purple_Lantern" },
        { "blue_torch", "defaulttorchblue" },
        { "blue_torch_side", "defaulttorchsideblue" },

        //DOORS
        { "locked_door", "SimpleLockedDoor" },
        { "default_door_vertical", "IronWoodDoor_Vertical" },
        { "default_door_horizontal", "IronWoodDoor_Horizontal" },
        { "jail_cell_door", "jaildoor" },		// (NPC Jail Cell Door)

        //Enemies
        { "random_easy_keep_enemy", "castle_01_easy" },		// (Simple keep enemies, like bullats, pinheads, and such)
        { "random_medium_keep_enemy", "castle_02_medium" },		// (Medium difficulty keep enemies, like Shotgun Kin)
        { "random_hard_keep_enemy", "castle_03_hard" },		// (Hard Keep Enemies, like Gun Nuts)
        { "random_easy_gungeon_enemy", "gungeon_01_easy" },		// (Hard Keep Enemies, like Gun Nuts)
        { "random_medium_gungeon_enemy", "gungeon_02_medium" },		// (Hard Keep Enemies, like Gun Nuts)
        { "random_hard_gungeon_enemy", "gungeon_03_hard" },		// (Hard Keep Enemies, like Gun Nuts)
        { "random_medium_mines_enemy", "mines_02_medium" },
        { "random_red_blue_vet_shotgun", "shotgunguysallvariants" },		// (Is randomly either a Blue, Red, or Veteran Shotgun Kin)
        { "random_bulletkin", "bulletman_variety" },		// (A random enemy on roughly the same difficulty level as a bullet kin. Eg: Bullet Kin, Shotgun Kin, Minelets, Tankers, etc)
        { "random_bulletkin_skullet", "hollow_bulletman_cocktail" },		// (Is randomly either a Bullet Kin, AK47 Bullet Kin, Skullet, or Skullmet)
        { "random_redshotgun_bulletkin", "proceduraltable_01_castle" },		// (Is randomly either a Red Shotgun Kin, Bullet Kin, or Veteran Bullet Kin)
        { "random_blobulon_pinhead", "blob or grenade" },		// (Is randomly either a Blobulon or Pinhead)
        { "random_cubulon_appgunjurer", "cubulon_or_yellow wizard" },		// (Is randomly either a Cubulon or Apprentice Gunjurer)
        { "random_hollowpoint_appgunjurer", "ghost_or_yellowwizard" },		// (Is randomly either a Hollowpoint or Apprentice Gunjurer)
        { "random_bookllet", "angrybook_variety" },		// (Is usually a Bookllet, with a chance to be Blue or Green instead of red)
        { "random_tier_blob", "blobulon_tiers" },		// (Is randomly either a Blobulon, Blobuloid, or Blobulon)
        { "random_tier_poisonblob", "poisbulon variety" },		// (Is randomly either a Poisbulon, Poisbuloid, or Poisbulin)
        { "random_bullat", "bullat_variety" },		// (A Random Bullat Variant)
        { "random_bookllet_gigi_appgunjurer", "yellow_wizard_book_bird_combo" },		// (Is randomly either an Apprentice Gunjurer, Bookllet, or Gigi)
        { "random_kingbullat_chancebulon", "king bullat or chancebulon" },		// (Either a King Bullat or Chancebulon)
        { "random_gunnut_leadmaiden", "lead maiden or gunnut" },		// (Either a Lead Maiden or a Gun Nut)
        { "random_rubberkin_tazie", "rubberbullet or tazie" },		// (Is randomly either a Rubber Kin or Tazie)
        { "random_snipershell_professional", "rifleman or professional" },		// (Either a Sniper Shell or a Professional)
        { "random_cubulon_wizbang_skullet", "cube_n_wizbang_n_skullets" },		// (Either a Cubulon, Skullet, Skullmet, or Wizbang)
        { "random_cubulon_cubulead", "cubecocktail" },		// (Either a Cubulon or a Cubulead)
        { "random_gripmaster_redshotgun_blueshotgun", "gripmaster_for_castle" },		// (Is randomly either a Gripmaster, Red Shotgun Kin or Blue Shotgun Kin)
        { "random_sometimes_tarnisher_blueshotgun", "tarnisher sometimes" },		// (Spawns a Tarnisher, a Blue Shotgun Kin?, or nothing at all)
        { "random_sometimes_rubberkin", "maybe rubber bulletman" },		// (Spawns either a Rubber Kin or nothing at all)
        { "random_sometimes_pinhead", "maybe grenade guy" },		// (Spawns a Pinhead sometimes, and nothing otherwise)
        { "random_sometimes_gunsinger", "lead wizard_maybe" },		// (Spawns a Gunsinger sometimes, and nothing otherwise)
        { "random_sometimes_gripmaster", "gripmaster sometimes" },		// (Spawns a Gripmaster sometimes, and nothing otherwise)

        //#DECOR (USUALLY MINOR BREAKABLES)-----------------------------------

        { "inanimate_candle_kin", "bulletman_candle_a" },		// (a random candle shaped roughly like a Bullet Kin)
        { "random_candles", "candles" },		// (A cluster of a random number of candles)
        { "one_candle", "candle" },		// (always 1 candle)
        { "two_candles", "doublecandle" },		// (Always 2 candles)
        { "three_candles", "triplecandle" },		// (always 3 candles)
        { "random_sometimes_candles", "candles_chance" },		// (Same as 'candles' but there's a chance for the number to be 0)
        { "floating_candle", "floatingcandle" },		// (A single levitating Candle)
        { "bench_horizontal", "monk_bench_long" },		// (A horizontally placed bench)
        { "pink_bench", "pink_bench_001" },		// (A horizontally placed bench)
        { "pile_of_skulls", "catacombs_skull_pile" },
        { "big_skull_down", "big_skull_001" },		// (south-facing)
        { "big_skull_left", "big_skull_002" },		// (West facing)
        { "big_skull_right", "big_skull_003" },		// (East Facing)
        { "sitting_skeleton_left", "skeleton_left_sit_corner" },
        { "sitting_skeleton_right", "skeleton_right_sit_corner" },
        { "broken_pot", "default pot broken" },		// (a pre-broken pot)
        { "ash_pot", "forge_ash_pot_01" },		// (An ashen pot from the forge)
        { "maggot_pot", "hell pot full" },		// (A Hell Pot full of writhing maggots)
        { "urn", "default urn" },
        { "wine_rack", "wine_rack_001" },
        { "writhing_bulletkin", "writhing bulletman" },		// (A sick bullet kin writhing in pain from the Oubliette)
        { "barrelman_down", "barrelman_down" },		// (A bullet kin in a barrel with his primer sticking out)
        { "barrelman_up", "barrelman_up" },		// (As above, but the Bullet Kin is the other way around)
        { "metal_bar_barrel", "barrel full of rods" },		// (A metal barrel of iron rods from the Forge)
        { "bunsen_burner", "bunson_burner" },		// (A metal barrel of iron rods from the Forge)
        { "wood_pedestal", "wooden_pedestal_001" },		// (Some sort of lectern)
        { "cabinet", "wine_cabinet" },		// (Some sort of lectern)
        { "cartstub_east", "cartstub_east" },		// (the blocks used to stop minecarts in the mines. Indestructable)
        { "cartstub_west", "cartstub_west" },		// (the blocks used to stop minecarts in the mines. Indestructable)
        { "cartstub_north", "cartstub_north" },		// (the blocks used to stop minecarts in the mines. Indestructable)
        { "cartstub_south", "cartstub_south" },		// (the blocks used to stop minecarts in the mines. Indestructable)
        { "big_crate_vertical", "default crate long" },		// (vertical crate)
        { "big_crate_horizontal", "default crate wide" },		// (horizontal crate)
        { "pillory", "pillory" },		// (horizontal crate)
        { "statue_bullet", "ratfigure_bullet" },		// (horizontal crate)
        { "statue_convict", "ratfigure_convict" },		// (horizontal crate)
        { "statue_hunter", "ratfigure_hunter" },		// (horizontal crate)
        { "statue_pilot", "ratfigure_pilot" },		// (horizontal crate)
        { "statue_robot", "ratfigure_robot" },		// (horizontal crate)
        { "statue_marine", "ratfigure_soldier" },		// (horizontal crate)
        { "telescope", "telescope" },		// (horizontal crate)
        { "sold_out_card", "sign_soldout" },

        //#MAJOR DECOR (USUALLY UNBREAKABLE)----------------------------------

        { "random_sarcophagus", "sarcophogus" },		// (A random sarcophagus)
        { "sarcophagus_bulletshield", "sarcophagus_bulletshield" },
        { "sarcophagus_bulletsword", "sarcophagus_bulletsword" },
        { "sarcophagus_shotgunbook", "sarcophagus_shotgunbook" },
        { "sarcophagus_shotgunmace", "sarcophagus_shotgunmace" },
        { "kaliber_statue", "bulletkingoldwall_statue" },		// (A statue of Kaliber from the Old King's bossroom)
        { "gargoyle_blood_fountain", "bulletkingoldwall_blood_font" },		// (A gargoyle head spewing blood into a bowl, meant to be placed on the northern wall of the old king's bossroom)
        { "gatling_gull_nest", "gatlinggullnest" },		// (The Gatling Gull's Nest)
        { "glass_case_pedestal", "shop_specialcase" },		// (Glass Display case from the shop)
        { "round_shop_table", "shoptable" },		// (The round table from the shop)
        { "ratty_carpet", "ratty_carpet_001" },
        { "maintenance_sign", "elevatormaintenancesign" },
        //{ "loreye", "creepyeye_room" }, //Can be placed, but doesn't reeeeeally work very well.

        //#GENUINE INTERACTIBLES---------------------------------------------

        { "brazier", "brazier" },
        { "random_shrine", "whichshrinewillitbe" },		// (A random shrine)
        { "lonks_backpack", "npc_lostadventurer_backpack" },		// (Lunk's backpack)
        { "lonks_shield", "npc_lostadventurer_shield" },		// (Lunk's Shield)
        { "lonks_sword", "npc_lostadventurer_sword" },		// (Lunk's Sword)
        { "high_dragunfire_chest", "highdragunfire_chest_red" },		// (The High Dragunfire Chest)
        { "random_unlocked_chest", "secretroomfloorchestunlocked" },		// (Random unlocked chest)
        { "abbey_entrance_altar", "gungeoncreststairs" },		// (The altar on which you place the old crest, and the special sarcophagus concealing the stairs. Fully functional)

        //#NPCS-------------------------------------------------------------

        { "npc_synerscope_right", "npc_synerscope_left" },		// (A Synerscope facing right)
        { "npc_synerscope_left", "npc_synerscope_right" },		// (Same as above but facing left)
        { "npc_cultist_bowback", "cultistbaldbowback_cutout" },		// (A cultist with his hood down bowing away from the camera)
        { "npc_cultist_bowright", "cultistbaldbowbackleft_cutout" },		// (A cultist with his hood down bowing to the right)
        { "npc_cultist_bowleft", "cultistbaldbowbackright_cutout" },		// (A cultist with his hood down bowing to the left)
        { "npc_cultist_interactible", "cultistbaldbowleft_cutout" },		// (The main cultist, interactible and talk-to-able)
        { "npc_cultist_hooded_facingaway", "cultisthoodbowback_cutout" },		// (A cultist with his hood up facing away from the camera)
        { "npc_cultist_hooded_facingright", "cultisthoodbowleft_cutout" },		// (A cultist with his hood up facing right)
        { "npc_cultist_hooded_facingleft", "cultisthoodbowright_cutout" },		// (A cultist with his hood up facing left)
        { "random_npc", "shopannex_contents_01" },		// (Random NPC that can appear as secondary in the shop. Most merchants, Vampire, and Sell Creep)
        { "baby_dragun", "babydragunjail" },		// (Baby Dragun)
        { "rat_merchant", "merchant_rat_placeable" }, //The version of the rat that gives free stuff

        //#TRAPS-------------------------------------------------------------

        //{ "conveyor_belt_right", "conveyor_horizontal" },		// (A conveyor belt to the right)
        //{ "conveyor_vertical", "conveyor_vertical" },		// (A conveyor belt going upwards) DOESN'T ACTUALLY WORK
        { "web_circle", "webplaceable" },		// (A pool of Phaser Spider web-goop)

        //#LIGHTING EFFECTS--------------------------------------------------

        //{ "gatlinggullarenalight", "gatlinggullarenalight" },		// (A decorative shadow cast on the ground, as though the roof of the room contains a giant spinning turbine)
        //{ "godrays_placeable", "godrays_placeable" },		// (A beam of light from above, creating a blightly lit area on the ground. Seen in chest rooms)
        //{ "defaulttorch", "defaulttorch" },		// (A yellow torch for a northern wall)
        //{ "defaulttorchside", "defaulttorchside" },		// (a yellow torch for a western wall)
        //{ "defaulttorchpurple", "defaulttorchpurple" },		// (a purple torch for a northern wall)
        //{ "defaulttorchsidepurple", "defaulttorchsidepurple" },		// (a purple torch for a western wall)
        //{ "sconce_light", "sconce_light" },		// (a purple torch for a western wall)
        //{ "sconce_light_side", "sconce_light_side" },		// (a purple torch for a western wall)


        //We're no strangers to love.
        //You know the rules, and so do I.
        //A full commitment's what I'm thinking of
        //You wouldn't get this from any other guy
        //I just wanna tell you how I'm feeling
        //Gotta make you understand
        //Never gonna give you up
        //Never gonna let you down
        //Never gonna run around and desert you
        //Never gonna make you cry
        //Never gonna say goodbye
        //Never gonna tell a lie and hurt you
    };
}


