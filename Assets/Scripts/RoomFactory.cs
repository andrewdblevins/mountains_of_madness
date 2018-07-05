using System;
using System.Collections.Generic;


public class RoomFactory
{
    Random random = new Random();
    UnityEngine.Vector3 endAttractionLocation = new UnityEngine.Vector3(500f, 500f, 0);

    public Room MakeRoom(string roomDescriptor)
    {
        Room room = new Room();

        switch (roomDescriptor)
        {
            case "trap1":
                room.Initialize(MakeTrap(1), null);
                break;
            case "trap2":
                room.Initialize(MakeTrap(2), null);
                break;
            case "trap3":
                room.Initialize(MakeTrap(3), null);
                break;
            case "monster1":
                room.Initialize(null, MakeMosnter(1));
                break;
            case "monster2":
                room.Initialize(null, MakeMosnter(2));
                break;
            case "monster3":
                room.Initialize(null, MakeMosnter(3));
                break;
            case "treasure1":
                room.Initialize(MakeTreasure(2, Treasure.TreasureType.ONE_SHOT), null);
                break;
            case "treasure2":
                room.Initialize(MakeTreasure(4, Treasure.TreasureType.ONE_SHOT), null);
                break;
            case "treasure3":
                room.Initialize(MakeTreasure(6, Treasure.TreasureType.ONE_SHOT), null);
                break;
            case "fountain1":
                room.Initialize(MakeTreasure(1, Treasure.TreasureType.PERMANENT), null);
                break;
            case "fountain2":
                room.Initialize(MakeTreasure(2, Treasure.TreasureType.PERMANENT), null);
                break;
            case "fountain3":
                room.Initialize(MakeTreasure(3, Treasure.TreasureType.PERMANENT), null);
                break;
            case "start":
                room.SetAttractionLocation(UnityEngine.Vector3.zero);
                break;
            case "end":
                room.SetAttractionLocation(endAttractionLocation);
                break;
            case "hall":
                break;

        }
        return room;
    }

    private Treasure MakeTrap(int strength)
    {
        int trapType = random.Next(0, 5);
        switch (trapType)
        {
            // health
            case 0:
                return new Treasure(0, -GetRandomValue(strength), 0, 0, 0, Treasure.TreasureType.PERMANENT);
            // sanity
            case 1:
                return new Treasure(0, 0, -GetRandomValue(strength), 0, 0, Treasure.TreasureType.PERMANENT);
            // weakening
            case 2:
                return new Treasure(0, 0, 0, Math.Min(-GetRandomValue(strength) / 3, -1), 0, Treasure.TreasureType.PERMANENT);
            // gold stealing
            case 3:
                return new Treasure(-GetRandomValue(strength) * 2, 0, 0, 0, 0, Treasure.TreasureType.PERMANENT);
            // health and sanity
            case 4:
                return new Treasure(0, Math.Min(-GetRandomValue(strength) / 2, -1), Math.Min(-GetRandomValue(strength) / 2, -1), 0, 0, Treasure.TreasureType.PERMANENT);
            // mugging
            case 5:
                return new Treasure(-GetRandomValue(strength), Math.Min(-GetRandomValue(strength) / 2, -1), 0, 0, 0, Treasure.TreasureType.PERMANENT);

        }
        return new Treasure(0, -5, 0, 0, 0, Treasure.TreasureType.PERMANENT);
    }

    private Treasure MakeTreasure(int strength, Treasure.TreasureType treasureType)
    {
        int treasureStyle = random.Next(0, 5);
        switch (treasureStyle)
        {
            // health
            case 0:
                return new Treasure(0, GetRandomValue(strength), 0, 0, 0, treasureType);
            // sanity
            case 1:
                return new Treasure(0, 0, GetRandomValue(strength), 0, 0, treasureType);
            // weakening
            case 2:
                return new Treasure(0, 0, 0, Math.Max(GetRandomValue(strength) / 3, 2), 0, treasureType);
            // gold stealing
            case 3:
                return new Treasure(GetRandomValue(strength) * 2, 0, 0, 0, 0, treasureType);
            // health and sanity
            case 4:
                return new Treasure(0, Math.Max(GetRandomValue(strength) / 2, 1), Math.Max(GetRandomValue(strength) / 2, 1), 0, 0, treasureType);
            // gold and health
            case 5:
                return new Treasure(GetRandomValue(strength), Math.Max(GetRandomValue(strength) / 2, 1), 0, 0, 0, treasureType);
            // die strength
            case 6:
                return new Treasure(0, 0, 0, 0, Math.Max(GetRandomValue(strength) / 3, 1), treasureType);

        }
        return new Treasure(10, 0, 0, 0, 0, treasureType);
    }

    private Monster MakeMosnter(int strength)
    {
        int monsterType = random.Next(0, 2);
        switch (monsterType)
        {
            // health
            case 0:
                return new Monster(GetRandomValue(strength), 0, GetRandomValue(strength * 3), GetRandomValue(strength), 0, MakeTreasure(strength + 1, Treasure.TreasureType.ONE_SHOT));
            // sanity
            case 1:
                return new Monster(0, GetRandomValue(strength), GetRandomValue(strength * 3), 0, GetRandomValue(strength), MakeTreasure(strength + 1, Treasure.TreasureType.ONE_SHOT));
            // both
            case 2:
                return new Monster(Math.Max(GetRandomValue(strength) / 2, 1), Math.Max(GetRandomValue(strength) / 2, 1),
                    GetRandomValue(strength * 3), Math.Max(GetRandomValue(strength) / 2, 1), Math.Max(GetRandomValue(strength) / 2, 1),
                    MakeTreasure(strength + 1, Treasure.TreasureType.ONE_SHOT));
        }
        return new Monster(5, 5, 20, 5, 5, MakeTreasure(strength + 1, Treasure.TreasureType.ONE_SHOT));
    }

    private int GetRandomValue(int strength)
    {
        return (random.Next(strength + 1) + random.Next(strength + 1) + random.Next(strength + 1)) * strength;
    }
}