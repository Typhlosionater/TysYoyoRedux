using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using MonoMod.Cil;

namespace TysYoyoRedux.Projectiles
{
	public class TysYoyoReduxGlobalProjectile : GlobalProjectile
	{
		public override bool InstancePerEntity => true;

		public int HitEffectCooldown = 0;

		public int EoCEnragedTimer = 0;

		public bool ValorWispOrbitActive;

		public bool GradientPulseOrbitActive;

		public bool Code1ElectrostaticAuraActive;

		public bool Code2PlasmaticAuraActive;

		public Vector2 ValkyrieTargetedEnemy = new Vector2(0, 0);

		public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
		{
			//Vanilla Yoyo Effects + Debuffs that trigger on NPC hits
			if (projectile.aiStyle == 99 && ModContent.GetInstance<TysYoyoReduxConfigServer>().VanillaYoyoEffects == true)
			{
				//Artery Bloody life steal effect
				if (projectile.type == ProjectileID.CrimsonYoyo)
				{
					if (Main.rand.Next(2) == 0 && target.lifeMax > 5 && !Main.player[projectile.owner].moonLeech)
					{
						int healingAmount = hit.Damage / 10;
						if (healingAmount > 0 && !(Main.player[projectile.owner].lifeSteal <= 0f))
						{
							Main.player[Main.myPlayer].lifeSteal -= healingAmount;
							Projectile.NewProjectile(projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ProjectileID.VampireHeal, 0, 0, projectile.owner, projectile.owner, healingAmount);
						}
					}
				}

				//Amazon Poisons on Impact
				if (projectile.type == ProjectileID.JungleYoyo)
				{
					if (Main.rand.Next(5) == 0)
					{
						target.AddBuff(BuffID.Poisoned, 7 * 60);
					}
				}

				//Cascade produces a fiery explosion + sparks on impact
				if (projectile.type == ProjectileID.Cascade && HitEffectCooldown == 0)
				{
					Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<VanillaYoyoEffects.CascadeFieryExplosionProjectile>(), projectile.damage, projectile.knockBack, projectile.owner);

					int ProjectileAmount = 2 + Main.rand.Next(2);
					for (int d = 0; d < ProjectileAmount; d++)
					{
						Vector2 ProjectileVelocity = new Vector2(2, 2).RotatedByRandom(MathHelper.ToRadians(360));
						ProjectileVelocity *= Main.rand.NextFloat(0.8f, 1f);
						int FiredProjectile = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, ProjectileVelocity, 504, projectile.damage / 4, projectile.knockBack / 8, projectile.owner);
						Main.projectile[FiredProjectile].DamageType = DamageClass.Melee;
					}

					HitEffectCooldown = 18;
					projectile.netUpdate = true;
				}

				//Hel-Fire Produces flame torrents every 3 hits
				if (projectile.type == ProjectileID.HelFire && HitEffectCooldown == 0)
				{
					SoundEngine.PlaySound(SoundID.Item34, projectile.Center);

					Vector2 ProjectileVelocity = new Vector2(5, 5);
					float numberProjectiles = 12;
					float rotation = MathHelper.ToRadians(330 / 2);
					float randomization = Main.rand.Next(45);
					for (int i = 0; i < numberProjectiles; i++)
					{
						Vector2 IndividualProjectileVelocity = ProjectileVelocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
						IndividualProjectileVelocity = new Vector2(IndividualProjectileVelocity.X, IndividualProjectileVelocity.Y).RotatedBy(randomization);
						IndividualProjectileVelocity = new Vector2(IndividualProjectileVelocity.X, IndividualProjectileVelocity.Y).RotatedByRandom(MathHelper.ToRadians(10));
						IndividualProjectileVelocity *= Main.rand.NextFloat(0.6f, 1f);

						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, IndividualProjectileVelocity, ModContent.ProjectileType<VanillaYoyoEffects.HelFireFlameTorrentProjectile>(), projectile.damage, 0, projectile.owner);
					}

					HitEffectCooldown = 28;
					projectile.netUpdate = true;
				}

				//Yelets Envenoms on impact
				if (projectile.type == ProjectileID.Yelets)
				{
					if (Main.rand.Next(3) == 0)
					{
						target.AddBuff(BuffID.Venom, 8 * 60);
					}
				}

				//Red's Throw Inflicts a Random Debuff on impact
				if (projectile.type == ProjectileID.RedsYoyo)
				{
					int DebuffSelection = Main.rand.Next(16);
					switch (DebuffSelection)
					{
						case 0:
							target.AddBuff(BuffID.Confused, 5 * 60);
							break;
						case 1:
							target.AddBuff(BuffID.CursedInferno, 5 * 60);
							break;
						case 2:
							target.AddBuff(BuffID.Ichor, 5 * 60);
							break;
						case 3:
							target.AddBuff(BuffID.BetsysCurse, 5 * 60);
							break;
						case 4:
							target.AddBuff(BuffID.Midas, 5 * 60);
							break;
						case 5:
							target.AddBuff(BuffID.Poisoned, 5 * 60);
							break;
						case 6:
							target.AddBuff(BuffID.Venom, 5 * 60);
							break;
						case 7:
							target.AddBuff(BuffID.OnFire3, 5 * 60);
							break;
						case 8:
							target.AddBuff(BuffID.Frostburn2, 5 * 60);
							break;
						case 9:
							target.AddBuff(BuffID.ShadowFlame, 5 * 60);
							break;
						case 10:
							target.AddBuff(BuffID.Daybreak, 5 * 60);
							break;
						case 11:
							target.AddBuff(BuffID.Oiled, 5 * 60);
							break;
						case 12:
							target.AddBuff(BuffID.Lovestruck, 5 * 60);
							break;
						case 13:
							target.AddBuff(BuffID.Stinky, 5 * 60);
							break;
						case 14:
							target.AddBuff(BuffID.Slimed, 5 * 60);
							break;
						case 15:
							target.AddBuff(BuffID.GelBalloonBuff, 5 * 60);
							break;
					}
				}

				//Kraken Abyssal Tentacles Effect
				if (projectile.type == ProjectileID.Kraken && HitEffectCooldown == 0)
				{
					SoundEngine.PlaySound(SoundID.Item103, projectile.position);
					int RandomRot = Main.rand.Next(0, 360);
					for (int d = 0; d < 3; d++)
					{
						RandomRot += 120;
						if (RandomRot >= 360)
						{
							RandomRot -= 360;
						}
						Vector2 value18 = new Vector2(3, 3).RotatedBy(MathHelper.ToRadians(RandomRot + Main.rand.Next(-55, 55)));
						value18.Normalize();
						Vector2 value19 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
						value19.Normalize();
						value18 = value18 * 4f + value19;
						value18.Normalize();
						value18 *= new Vector2(5, 5);
						float num325 = (float)Main.rand.Next(10, 80) * 0.001f;
						if (Main.rand.Next(2) == 0)
						{
							num325 *= -1f;
						}
						float num326 = (float)Main.rand.Next(10, 80) * 0.001f;
						if (Main.rand.Next(2) == 0)
						{
							num326 *= -1f;
						}
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, value18, ModContent.ProjectileType<VanillaYoyoEffects.KrakenAbyssalTentacleProjectile>(), projectile.damage, projectile.knockBack, projectile.owner, num326, num325);
					}

					HitEffectCooldown = 13;
					projectile.netUpdate = true;
				}

				//Eye of Cthulhu enrages when striking enemies
				if (projectile.type == ProjectileID.TheEyeOfCthulhu)
				{
					EoCEnragedTimer = 30;
				}
			}
		}

		public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
		{
			//Vanilla Yoyo Effects + Debuffs that trigger on Player hits
			if (projectile.aiStyle == 99 && ModContent.GetInstance<TysYoyoReduxConfigServer>().VanillaYoyoEffects == true)
			{
				//Artery Bloody life steal effect
				if (projectile.type == ProjectileID.CrimsonYoyo)
				{
					if (Main.rand.Next(2) == 0 && !Main.player[projectile.owner].moonLeech)
					{
						int healingAmount = info.Damage / 10;
						if (healingAmount > 0 && !(Main.player[projectile.owner].lifeSteal <= 0f))
						{
							Main.player[Main.myPlayer].lifeSteal -= healingAmount;
							Projectile.NewProjectile(projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ProjectileID.VampireHeal, 0, 0, projectile.owner, projectile.owner, healingAmount);
						}
					}
				}

				//Amazon Poisons on Impact
				if (projectile.type == ProjectileID.JungleYoyo)
				{
					if (Main.rand.Next(5) == 0)
					{
						target.AddBuff(BuffID.Poisoned, 7 * 60);
					}
				}

				//Cascade produces a fiery explosion + sparks on impact
				if (projectile.type == ProjectileID.Cascade && HitEffectCooldown == 0)
				{
					Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<VanillaYoyoEffects.CascadeFieryExplosionProjectile>(), projectile.damage, projectile.knockBack, projectile.owner);

					int ProjectileAmount = 2 + Main.rand.Next(2);
					for (int d = 0; d < ProjectileAmount; d++)
					{
						Vector2 ProjectileVelocity = new Vector2(2, 2).RotatedByRandom(MathHelper.ToRadians(360));
						ProjectileVelocity *= Main.rand.NextFloat(0.8f, 1f);
						int FiredProjectile = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, ProjectileVelocity, 504, projectile.damage / 4, projectile.knockBack / 8, projectile.owner);
						Main.projectile[FiredProjectile].DamageType = DamageClass.Melee;
					}

					HitEffectCooldown = 18;
					projectile.netUpdate = true;
				}

				//Hel-Fire Produces flame torrents every 3 hits
				if (projectile.type == ProjectileID.HelFire && HitEffectCooldown == 0)
				{
					SoundEngine.PlaySound(SoundID.Item34, projectile.Center);

					Vector2 ProjectileVelocity = new Vector2(5, 5);
					float numberProjectiles = 12;
					float rotation = MathHelper.ToRadians(330 / 2);
					float randomization = Main.rand.Next(45);
					for (int i = 0; i < numberProjectiles; i++)
					{
						Vector2 IndividualProjectileVelocity = ProjectileVelocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
						IndividualProjectileVelocity = new Vector2(IndividualProjectileVelocity.X, IndividualProjectileVelocity.Y).RotatedBy(randomization);
						IndividualProjectileVelocity = new Vector2(IndividualProjectileVelocity.X, IndividualProjectileVelocity.Y).RotatedByRandom(MathHelper.ToRadians(10));
						IndividualProjectileVelocity *= Main.rand.NextFloat(0.6f, 1f);

						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, IndividualProjectileVelocity, ModContent.ProjectileType<VanillaYoyoEffects.HelFireFlameTorrentProjectile>(), projectile.damage, 0, projectile.owner);
					}

					HitEffectCooldown = 28;
					projectile.netUpdate = true;
				}

				//Yelets Envenoms on impact
				if (projectile.type == ProjectileID.Yelets)
				{
					if (Main.rand.Next(3) == 0)
					{
						target.AddBuff(BuffID.Venom, 8 * 60);
					}
				}

				//Red's Throw Inflicts a Random Debuff on impact
				if (projectile.type == ProjectileID.RedsYoyo)
				{
					int DebuffSelection = Main.rand.Next(11);
					switch (DebuffSelection)
					{
						case 0:
							target.AddBuff(BuffID.Confused, 5 * 60);
							break;
						case 1:
							target.AddBuff(BuffID.CursedInferno, 5 * 60);
							break;
						case 2:
							target.AddBuff(BuffID.Ichor, 5 * 60);
							break;
						case 3:
							target.AddBuff(BuffID.Poisoned, 5 * 60);
							break;
						case 4:
							target.AddBuff(BuffID.Venom, 5 * 60);
							break;
						case 5:
							target.AddBuff(BuffID.OnFire3, 5 * 60);
							break;
						case 6:
							target.AddBuff(BuffID.Frostburn2, 5 * 60);
							break;
						case 7:
							target.AddBuff(BuffID.Lovestruck, 5 * 60);
							break;
						case 8:
							target.AddBuff(BuffID.Stinky, 5 * 60);
							break;
						case 9:
							target.AddBuff(BuffID.Slimed, 5 * 60);
							break;
						case 10:
							target.AddBuff(BuffID.GelBalloonBuff, 5 * 60);
							break;
					}
				}

				//Kraken Abyssal Tentacles Effect
				if (projectile.type == ProjectileID.Kraken && HitEffectCooldown == 0)
				{
					SoundEngine.PlaySound(SoundID.Item103, projectile.position);
					int RandomRot = Main.rand.Next(0, 360);
					for (int d = 0; d < 3; d++)
					{
						RandomRot += 120;
						if (RandomRot >= 360)
						{
							RandomRot -= 360;
						}
						Vector2 value18 = new Vector2(3, 3).RotatedBy(MathHelper.ToRadians(RandomRot + Main.rand.Next(-55, 55)));
						value18.Normalize();
						Vector2 value19 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
						value19.Normalize();
						value18 = value18 * 4f + value19;
						value18.Normalize();
						value18 *= new Vector2(5, 5);
						float num325 = (float)Main.rand.Next(10, 80) * 0.001f;
						if (Main.rand.Next(2) == 0)
						{
							num325 *= -1f;
						}
						float num326 = (float)Main.rand.Next(10, 80) * 0.001f;
						if (Main.rand.Next(2) == 0)
						{
							num326 *= -1f;
						}
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, value18, ModContent.ProjectileType<VanillaYoyoEffects.KrakenAbyssalTentacleProjectile>(), projectile.damage, projectile.knockBack, projectile.owner, num326, num325);
					}

					HitEffectCooldown = 13;
					projectile.netUpdate = true;
				}

				//Eye of Cthulhu enrages when striking enemies
				if (projectile.type == ProjectileID.TheEyeOfCthulhu)
				{
					EoCEnragedTimer = 30;
				}
			}
		}

		//Constant effects
		public override void AI(Projectile projectile)
		{
			//Vanilla Yoyo Effects that trigger passively
			if (projectile.aiStyle == 99 && ModContent.GetInstance<TysYoyoReduxConfigServer>().VanillaYoyoEffects == true)
			{
                //On hit effect cooldown
                if (HitEffectCooldown > 0)
				{
					HitEffectCooldown--;
					projectile.netUpdate = true;
				}

				//Rally Afterimages
				if (projectile.type == ProjectileID.Rally && projectile.owner == Main.myPlayer)
				{
					projectile.frameCounter++;
					if (projectile.frameCounter >= 2)
					{
						projectile.frameCounter = 0;
						projectile.netUpdate = true;

						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<VanillaYoyoEffects.RallyAfterimageProjectile>(), projectile.damage / 2, 0, projectile.owner);
					}
				}

				//Malaise Bile Drops
				if (projectile.type == ProjectileID.CorruptYoyo)
				{
					projectile.frameCounter++;
					if (projectile.frameCounter >= 10 + Main.rand.Next(16) && projectile.owner == Main.myPlayer)
					{
						projectile.frameCounter = 0;
						projectile.netUpdate = true;

						Projectile.NewProjectile(projectile.GetSource_FromThis(), (projectile.position.X + 2 + Main.rand.Next(8)), (projectile.position.Y + 2 + Main.rand.Next(8)), 0f, 0f, ModContent.ProjectileType<VanillaYoyoEffects.MalaiseBileDropletProjectile>(), projectile.damage / 3, 0.5f, projectile.owner);
					}
				}

				//Amazon randomly fires stringers
				if (projectile.type == ProjectileID.JungleYoyo)
				{
					projectile.frameCounter++;
					if (projectile.frameCounter >= 15 + Main.rand.Next(6) && projectile.owner == Main.myPlayer)
					{
						projectile.frameCounter = 0;
						projectile.netUpdate = true;

						Vector2 ProjectileVelocity = new Vector2(8, 8).RotatedByRandom(MathHelper.ToRadians(360));
						ProjectileVelocity *= Main.rand.NextFloat(0.6f, 1f);
						int FiredProjectile = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, ProjectileVelocity, ProjectileID.HornetStinger, projectile.damage / 2, projectile.knockBack / 4, projectile.owner);
						Main.projectile[FiredProjectile].timeLeft = 15;
						Main.projectile[FiredProjectile].DamageType = DamageClass.Melee;
					}
				}

				//Code 1 Electrostatic Aura
				if (projectile.type == ProjectileID.Code1)
				{
					if (!this.Code1ElectrostaticAuraActive && projectile.owner == Main.myPlayer)
					{
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<VanillaYoyoEffects.Code1ElectrostaticAuraProjectile>(), projectile.damage / 3, 0f, projectile.owner, 0f, projectile.whoAmI);
						this.Code1ElectrostaticAuraActive = true;
					}
				}

				//Valor is orbited by 3 ghostly wisp projectiles
				if (projectile.type == ProjectileID.Valor)
				{
					if (!this.ValorWispOrbitActive && projectile.owner == Main.myPlayer)
					{
						for (int d = 0; d < 3; d++)
						{
							Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<VanillaYoyoEffects.ValorGhostlyWispProjectile>(), projectile.damage, projectile.knockBack / 4, projectile.owner, d, projectile.whoAmI);
						}
						this.ValorWispOrbitActive = true;
					}
				}

				//Gradient is orbited by 5 chromatic pulse projectiles
				if (projectile.type == ProjectileID.Gradient)
				{
					if (!this.GradientPulseOrbitActive && projectile.owner == Main.myPlayer)
					{
						for (int d = 0; d < 5; d++)
						{
							Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<VanillaYoyoEffects.GradientChromaticPulseProjectile>(), projectile.damage, projectile.knockBack / 4, projectile.owner, d, projectile.whoAmI);
						}
						this.GradientPulseOrbitActive = true;
					}
				}

				//Chik rapidly & randomly produces crystal shards
				if (projectile.type == ProjectileID.Chik)
				{
					if (Main.rand.Next(8) == 0 && projectile.owner == Main.myPlayer)
                    {
						float ShardXVelocity = (0f - projectile.velocity.X) * (float)Main.rand.Next(40, 70) * 0.01f + (float)Main.rand.Next(-20, 21) * 0.4f;
						float ShardYVelocity = (0f - projectile.velocity.Y) * (float)Main.rand.Next(40, 70) * 0.01f + (float)Main.rand.Next(-20, 21) * 0.4f;
						int FiredProjectile = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, ShardXVelocity / 2, ShardYVelocity / 2, ProjectileID.CrystalShard, projectile.damage / 5, 0f, projectile.owner);
						Main.projectile[FiredProjectile].DamageType = DamageClass.Melee;
					}
				}

				//Amarok Snowflakes
				if (projectile.type == ProjectileID.Amarok)
				{
					projectile.frameCounter++;
					if (projectile.frameCounter >= 30 && projectile.owner == Main.myPlayer)
					{
						projectile.frameCounter = 0;
						projectile.netUpdate = true;

						SoundEngine.PlaySound(SoundID.Item27, projectile.Center);
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<VanillaYoyoEffects.AmarokSnowflakeProjectile>(), projectile.damage, 0, projectile.owner);
					}
				}

				//Code 2 Plasmatic Aura
				if (projectile.type == ProjectileID.Code2)
				{
					if (!this.Code2PlasmaticAuraActive && projectile.owner == Main.myPlayer)
					{
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<VanillaYoyoEffects.Code2PlasmaticAuraProjectile>(), projectile.damage / 2, 0f, projectile.owner, 0f, projectile.whoAmI);
						this.Code2PlasmaticAuraActive = true;
					}
				}

				//Yelets Randomly Fires Razor Leaves
				if (projectile.type == ProjectileID.Yelets)
				{
					projectile.frameCounter++;
					if (projectile.frameCounter >= 10 + Main.rand.Next(11) && projectile.owner == Main.myPlayer)
					{
						projectile.frameCounter = 0;
						projectile.netUpdate = true;

						SoundEngine.PlaySound(SoundID.Item7, projectile.Center);
						Vector2 ProjectileVelocity = new Vector2(10, 10).RotatedByRandom(MathHelper.ToRadians(360));
						ProjectileVelocity *= Main.rand.NextFloat(0.75f, 1f);
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, ProjectileVelocity, ModContent.ProjectileType<VanillaYoyoEffects.YeletsRazorLeafProjectile>(), projectile.damage / 2, projectile.knockBack / 3, projectile.owner);
					}
				}

				//Valkyrie Feather Barrage
				if (projectile.type == ProjectileID.ValkyrieYoyo)
				{
					projectile.frameCounter++;
					if ((projectile.frameCounter >= 45) && (ValkyrieTargetedEnemy == new Vector2(0, 0)))
                    {
						//Select Attack Target
						float AttackRange = 600f;
						for (int NpcCheck = 0; NpcCheck < 200; NpcCheck++)
						{
							NPC nPC = Main.npc[NpcCheck];
							if (Main.npc[NpcCheck].CanBeChasedBy(this, ignoreDontTakeDamage: true) && Vector2.Distance(projectile.Center, nPC.Center) <= AttackRange && Collision.CanHit(projectile.Center, 1, 1, Main.npc[NpcCheck].Center, 1, 1))
							{
								if (Vector2.Distance(projectile.Center, nPC.Center) < Vector2.Distance(projectile.Center, ValkyrieTargetedEnemy))
								{
									ValkyrieTargetedEnemy = nPC.Center;
								}
							}
						}

						projectile.frameCounter = 45;
					}
					if (((projectile.frameCounter == 45) || (projectile.frameCounter == 60) || (projectile.frameCounter == 75)) && (ValkyrieTargetedEnemy != new Vector2(0, 0)) && projectile.owner == Main.myPlayer)
					{
						//Actually Fire
						Vector2 ProjectileVelocity = ValkyrieTargetedEnemy - projectile.Center;
						ProjectileVelocity.Normalize();
						ProjectileVelocity *= 6;
						ProjectileVelocity = ProjectileVelocity.RotatedByRandom(MathHelper.ToRadians(8 + Main.rand.Next(9)));

						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, ProjectileVelocity, ModContent.ProjectileType<VanillaYoyoEffects.ValkyrieYoyoFeatherProjectile>(), projectile.damage / 2, projectile.knockBack / 5, projectile.owner);
					}
					if ((projectile.frameCounter >= 75) && (ValkyrieTargetedEnemy != new Vector2(0, 0)))
					{
						ValkyrieTargetedEnemy = new Vector2(0, 0);

						projectile.frameCounter = 0;
						projectile.netUpdate = true;
					}
				}

				//Eye of Cthulhu Produces mini servants of cthulhu
				if (projectile.type == ProjectileID.TheEyeOfCthulhu)
				{
					projectile.frameCounter++;

					//When you hit enemies it speeds up the rate at which the yoyo produces eyes temporarily
					if (EoCEnragedTimer > 0)
                    {
						EoCEnragedTimer--;

						if (projectile.frameCounter >= 20 + Main.rand.Next(11) && projectile.owner == Main.myPlayer)
						{
							projectile.frameCounter = 0;
							projectile.netUpdate = true;

							Vector2 ProjectileVelocity = new Vector2(2, 2).RotatedByRandom(MathHelper.ToRadians(360));
							ProjectileVelocity *= Main.rand.NextFloat(0.8f, 1f);
							Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, ProjectileVelocity, ModContent.ProjectileType<VanillaYoyoEffects.EoCMiniServantProjectile>(), projectile.damage / 5, projectile.knockBack / 8, projectile.owner);
						}
					}
                    else
                    {
						if (projectile.frameCounter >= 35 + Main.rand.Next(11) && projectile.owner == Main.myPlayer)
						{
							projectile.frameCounter = 0;
							projectile.netUpdate = true;

							Vector2 ProjectileVelocity = new Vector2(2, 2).RotatedByRandom(MathHelper.ToRadians(360));
							ProjectileVelocity *= Main.rand.NextFloat(0.8f, 1f);
							Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, ProjectileVelocity, ModContent.ProjectileType<VanillaYoyoEffects.EoCMiniServantProjectile>(), projectile.damage / 5, projectile.knockBack / 8, projectile.owner);
						}
					}
				}
			}
		}

		private float oldYoyoLifeTimeMult = 1f;

		//Yoyo bearings effect
		public override bool PreAI(Projectile projectile)
		{
			oldYoyoLifeTimeMult = ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type];

			Player owner = Main.player[projectile.owner];
			if (owner.GetModPlayer<TysYoyoReduxPlayer>().YoyoBearings)
			{
				ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = oldYoyoLifeTimeMult * 1.5f;
			}

			return base.PreAI(projectile);
		}

		public override void PostAI(Projectile projectile)
		{
			ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = oldYoyoLifeTimeMult;
		}

		//Damage modification effects
		public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
		{
			//Format:C double critical strike damage effect
			if (projectile.type == ProjectileID.FormatC && ModContent.GetInstance<TysYoyoReduxConfigServer>().VanillaYoyoEffects == true)
			{
				modifiers.CritDamage += 1f;
			}
		}

		//Counterweight Changes
        public override void SetDefaults(Projectile projectile)
        {
            if (projectile.type >= 556 && projectile.type <= 561 && ModContent.GetInstance<TysYoyoReduxConfigServer>().BuffsToCounterweights == true)
            {
				projectile.usesLocalNPCImmunity = true;
				projectile.localNPCHitCooldown = 10;
				projectile.extraUpdates = 1;
			}
        }

		//AI overrides
        public override void Load()
        {
	        // Make counterweights swap out every few seconds
	        IL_Projectile.AI_099_1 += il =>
	        {
		        var cursor = new ILCursor(il);

		        cursor.GotoNext(MoveType.After,
			        i => i.MatchLdsfld<Main>(nameof(Main.player)),
			        i => i.MatchLdarg0(),
			        i => i.MatchLdfld<Projectile>(nameof(Projectile.owner)),
			        i => i.MatchLdelemRef(),
			        i => i.MatchLdfld<Player>(nameof(Player.channel)));

		        cursor.EmitLdarg0();
		        cursor.EmitDelegate((Projectile proj) =>
		        {
			        return proj.frameCounter < (int)(60 * 2 * Main.rand.NextFloat(4f, 6f));
		        });

		        cursor.EmitAnd();
	        };
        }
	}
}