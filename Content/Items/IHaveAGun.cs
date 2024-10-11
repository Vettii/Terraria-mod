using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace TestingMod.Content.Items
{ 
	public class IHaveAGun : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 420;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 60;
			Item.useAnimation = 60;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 10;
			Item.value = Item.buyPrice(silver: 1);
			Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item36;
			Item.autoReuse = false;

            // Gun Properties
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.Bullet;

			Item.UseSound = new SoundStyle($"{nameof(TestingMod)}/Assets/Sounds/gunshot-meme") {
				Volume = 0.9f,
				PitchVariance = 0.2f,
				MaxInstances = 10,
			};
		}

		 public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 64f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;

            int dustQuantity = 10; // How many particles do you want ?

            for(int i = 0 ; i < dustQuantity ; i ++)

            {
                Vector2 dustOffset = Vector2.Normalize(velocity) * 32f;
                int dust = Dust.NewDust(player.position + dustOffset, Item.width, Item.height, DustID.Torch); // Create the dust, "6" is the dust type (fire, in that case)
				
                Main.dust[dust].noGravity = true; // Is the dust affected by gravity ?
                Main.dust[dust].velocity *= 10f;    // Change the dust velocity.
                Main.dust[dust].scale = 3f;    // Change the dust size.
            }
            return true; // Do you want the weapon to shoot the initial projectile ?
        }


        // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
		public override Vector2? HoldoutOffset() {
			return new Vector2(2f, -2f);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}



    }
}