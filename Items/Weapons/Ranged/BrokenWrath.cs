using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;
using Stellamod.Projectiles.Gun;
using Stellamod.Items.Materials;
using Stellamod.Items.Materials.Tech;
using Terraria.Audio;
using Stellamod.Items.Harvesting;

namespace Stellamod.Items.Weapons.Ranged
{
	public class BrokenWrath : ModItem
	{
		public override void SetStaticDefaults() 
		{
            // DisplayName.SetDefault("Broken Wrath");
		}

		public override void SetDefaults() 
		{
			Item.damage = 23;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 32;
			Item.useAnimation = 32;
			Item.useStyle = 5;
			Item.knockBack = 6;
			Item.value = Item.sellPrice(0, 0, 20, 0);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item5;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<BrokenMissile>();
			Item.shootSpeed = 4f;
			Item.useAmmo = AmmoID.Bullet;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-2, 0);
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (type == ProjectileID.Bullet) type = ModContent.ProjectileType<BrokenMissile>();
			Vector2 Offset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y - 1)) * 20f;
			if (Collision.CanHit(position, 0, 0, position + Offset, 0, 0))
			{
				position += Offset;
			}

			velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
			Projectile proj = Projectile.NewProjectileDirect(source, position, velocity, type, Item.damage, Item.knockBack, Item.playerIndexTheItemIsReservedFor, 0, 0);
			proj.netUpdate = true;
            for (int index1 = 0; index1 < 19; ++index1)
            {
                int index2 = Dust.NewDust(new Vector2(position.X, position.Y), Item.width - 20, Item.height - 45, 244, velocity.X, velocity.Y, (int)byte.MaxValue, new Color(), (float)Main.rand.Next(6, 10) * 0.1f);
                Main.dust[index2].noGravity = true;
                Main.dust[index2].velocity *= 0.5f;
                Main.dust[index2].scale *= 1.2f;
            }
			damage = this.Item.damage / 2;
			Vector2 origVect = new Vector2(velocity.X, velocity.Y);
            //generate the remaining projectiles
            int Sound = Main.rand.Next(1, 3);
            if (Sound == 1)
            {
                SoundEngine.PlaySound(new SoundStyle("Stellamod/Assets/Sounds/BrokenWrath2"), player.position);
            }
            else
            {
                SoundEngine.PlaySound(new SoundStyle("Stellamod/Assets/Sounds/BrokenWrath1"), player.position);
            }
            Main.LocalPlayer.GetModPlayer<MyPlayer>().ShakeAtPosition(player.position, 248f, 14f);
            Vector2 newVect = origVect.RotatedBy(System.Math.PI / (Main.rand.Next(72, 1300) / 23));
			Projectile.NewProjectile(source, position.X, position.Y, newVect.X, newVect.Y, Mod.Find<ModProjectile>("BTech1").Type, damage, Item.knockBack, player.whoAmI, 0f, 0f);
			newVect = origVect.RotatedBy(System.Math.PI / (Main.rand.Next(72, 1300) / 23));
			Projectile.NewProjectile(source, position.X, position.Y, newVect.X, newVect.Y, Mod.Find<ModProjectile>("BTech2").Type, damage, Item.knockBack, player.whoAmI, 0f, 0f);
			newVect = origVect.RotatedBy(System.Math.PI / (Main.rand.Next(72, 1300) / 23));
			Projectile.NewProjectile(source, position.X, position.Y, newVect.X, newVect.Y, Mod.Find<ModProjectile>("BTech3").Type, damage, Item.knockBack, player.whoAmI, 0f, 0f);
			newVect = origVect.RotatedBy(System.Math.PI / (Main.rand.Next(72, 1300) / 13));

			return false;
		}
		public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemType<ArnchaliteBar>(), 20);
			recipe.AddIngredient(ItemType<Cinderscrap>(), 10);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}