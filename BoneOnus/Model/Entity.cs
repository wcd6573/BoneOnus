using System;
using System.Collections.Generic;
using BoneOnus;
using BoneOnus.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Entity
{
	List<BoneType> bones;
	Texture2D texture;
	Weapon weapon;
	int health;

	public Entity(List<BoneType> bones, Texture2D texture, Weapon weapon, int health)
	{
		this.bones = bones;
		this.texture = texture;
		this.weapon = weapon;
		this.health = health;
	}

	public Entity(List<BoneType> bones, Texture2D texture, int health)
	{
		this.bones = bones;
		this.texture = texture;
		this.health = health;
	}

	public void Attack(Entity entity)
	{
		//TODO: add attack logic
	}

	public void Equip(Weapon weapon)
	{
		this.weapon = weapon;
	}

	public void TakeDamage(int damage)
	{
		this.health -= damage;
	}

	void Update(GameTime gameTime)
	{
		// TODO: Add your update logic here
	}

	void Draw(GameTime gameTime)
	{
		// TODO: Add your draw logic here
	}
}
