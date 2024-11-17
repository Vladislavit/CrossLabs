namespace Lab3;

public class Labyrinth
{
    private readonly Dictionary<int, Dictionary<int, int>> _roomList;
    private readonly string[] _instructions;
    private readonly int _n;
    private readonly int _m;

    public Labyrinth(string[] instructions)
    {
        _instructions = instructions;
        _roomList = new Dictionary<int, Dictionary<int, int>>();

        (_n, _m) = GetRoomsAndCorridors();
        InitializeRooms();
        GenerateLabyrinth();
    }

    private (int, int) GetRoomsAndCorridors()
    {
        string[] firstLine = _instructions[0].Split(' ');
        if (firstLine.Length != 2)
        {
            throw new Exception($"Invalid first line: {firstLine.Length} != 2");
        }
        if (!int.TryParse(firstLine[0], out int rooms) || rooms is < 1 or > 10000 ||
            !int.TryParse(firstLine[1], out int corridors) || corridors is < 1 or > 100000)
            throw new Exception("Invalid labyrinth data");
        return (rooms, corridors);
    }

    private void GenerateLabyrinth()
    {
        for (int i = 1; i <= _m; i++)
        {
            string[] corridorData = _instructions[i].Split(' ');
            if (!int.TryParse(corridorData[0], out int u) || !(u >= 1 && u <= _n) ||
                !int.TryParse(corridorData[1], out int v) || !(v >= 1 && v <= _n) ||
                !int.TryParse(corridorData[2], out int c) || !(c is >= 1 and <= 100))
                throw new Exception("Invalid corridor data");
            AddCorridor(u, v, c);
        }
    }


    public void InitializeRooms()
    {
        for (int i = 1; i <= _n; i++)
            _roomList[i] = new Dictionary<int, int>();
    }

    public void AddCorridor(int room1, int room2, int color)
    {
        if (_roomList[room1].ContainsKey(color) || _roomList[room2].ContainsKey(color))
        {
            throw new Exception($"One room can't have more than one corridor with color {color}");
        }
        _roomList[room1][color] = room2;
        _roomList[room2][color] = room1;
    }

    public string GetResult()
    {
        int currentRoom = CurrentRoom();
        return currentRoom == 0 ? "INCORRECT" : currentRoom.ToString();
    }

    public int CurrentRoom()
    {
        if (!int.TryParse(_instructions[_m + 1], out int k) || k is < 0 or > 100000)
            throw new Exception("Invalid path data");
        if (k == 0)
            return 1;

        string[] path = _instructions[_m + 2].Split(' ');
        int currentRoom = 1;
        for (int i = 0; i < k; i++)
        {
            if (!int.TryParse(path[i], out int color))
            {
                throw new Exception($"Invalid color: {path[i]}");
            }
            if (!_roomList[currentRoom].ContainsKey(color))
                return 0;
            currentRoom = _roomList[currentRoom][color];
        }
        return currentRoom;
    }
}
