using System;
using System.Collections.Generic;

public class Entity
{
	List<Bone> bone;
	Texture2D texture;
	Weapon weapon;
	int health;

	public Entity(List<Bone> bone, Texture2D texture, Weapon weapon, int health)
	{
		this.bone = bone;
		this.texture = texture;
		this.weapon = weapon;
		this.health = health;
	}

    public Entity(List<Bone> bone, Texture2D texture, int health)
    {
        this.bone = bone;
        this.texture = texture;
        this.health = health;
    }

    private void Attack(Entity entity)
	{

	}
	private void Equip(Weapon weapon)
	{
		this.weapon = weapon;
	}

	private void TakeDamage(int damage)
	{
		this.health -= damage;
	}

    void Update(GameTime gameTime)
    {
        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin();
    }
