using System;
using System.Runtime.CompilerServices;

class Box
{
    public int h, w, d, area;
    public Box(int h, int w, int d)
    {
        this.h = h;
        this.w = w;
        this.d = d;
    }

    public bool IsSmallerThan(Box other)
    {
        return this.w * this.d < other.w * other.d;
    }
}

class Heaps
{
    public static int MaxStackHeight(List<Box> listOfBoxes, int count)
    {
        int max = -1;
        int maxOld = -1;
        int position = 0;

        for (int i = 0; i < count; i++)
            listOfBoxes[i].area = listOfBoxes[i].w * listOfBoxes[i].d;

        List<Box> sortedList = listOfBoxes.OrderByDescending(o => o.area).ToList();

        int[] msh = new int[count];
        List<List<Box>> boxCombination = new List<List<Box>>();
        for (int i = 0; i < count; i++)
        {
            msh[i] = sortedList[i].h;
            List<Box> temp = new List<Box>();
            temp.Add(sortedList[i]);
            boxCombination.Add(temp);
        }
        for (int i = 0; i < count; i++)
        {
            msh[i] = 0;
            Box box = sortedList[i];
            int val = 0;
            List<Box> boxes = new List<Box>();
            for (int j = 0; j < i; j++)
            {
                Box prevBox = sortedList[j];
                if (box.w <= prevBox.w && box.d <= prevBox.d)
                {
                    val = Math.Max(val, msh[j]);
                    boxes = boxes.Sum(x => x.h) > boxCombination[j].Sum(y => y.h) ? boxes.ToList() : boxCombination[j].ToList();
                }
            }
            msh[i] = val + box.h;
            boxes.Add(sortedList[i]);
            boxCombination[i] = boxes;
        }
        for (int i = 0; i < count; i++)
        {
            maxOld = max;
            max = Math.Max(max, msh[i]);
            if (maxOld != max) position = i;
        }
        Console.WriteLine("-----------------------------------------------------------");
        Console.WriteLine("Boxes in maximum posible stack: ");
        for (int i = 0; i < boxCombination[position].Count(); i++) {
            
                Console.WriteLine("{" + boxCombination[position][i].d + ", " + boxCombination[position][i].w + ", " + boxCombination[position][i].h + "}");
        }
        return max;
    }

    public static void LeastNumberOfHeaps(List<Box> listOfBoxes, int count, List<List<Box>> heaps, int numberOfHeaps)
    {
        int max = -1;
        int oldMax = -1;
        int position = 0;
        int stackedBoxes = 0;

        for (int i = 0; i < count; i++)
            listOfBoxes[i].area = listOfBoxes[i].w * listOfBoxes[i].d;

         List<Box> sortedList = listOfBoxes.OrderByDescending(o=>o.area).ToList();

        int[] msh = new int[count];
        List<List<Box>> boxCombination = new List<List<Box>>();

        for (int i = 0; i < count; i++)
        {
            msh[i] = sortedList[i].h;
            List<Box> temp = new List<Box>();
            temp.Add(sortedList[i]);
            boxCombination.Add(temp);
        }

        for (int i = 0; i < count; i++)
        {
            msh[i] = 0;
            Box box = sortedList[i];
            int val = 0;
            List<Box> boxes = new List<Box>();
            for (int j = 0; j < i; j++)
            {
                Box prevBox = sortedList[j];
                if (box.w <= prevBox.w && box.d <= prevBox.d)
                {
                    val = Math.Max(val, msh[j]);
                    boxes = boxes.Sum(x => x.h) > boxCombination[j].Sum(y => y.h) ? boxes.ToList() : boxCombination[j].ToList();
                }
            }
            msh[i] = val + box.h;
            boxes.Add(sortedList[i]);
            boxCombination[i] = boxes;
        }
        for (int i = 0; i < count; i++)
        {
            oldMax = max;
            max = Math.Max(max, msh[i]);
            if (oldMax != max) position = i;
        }
        ++numberOfHeaps;
        stackedBoxes += boxCombination[position].Count();
        heaps.Add(boxCombination[position]);
        for (int i = 0; i < boxCombination[position].Count(); i++)
        {
            Console.WriteLine("{" + boxCombination[position][i].d + ", " + boxCombination[position][i].w + ", " + boxCombination[position][i].h + "}");
        }
        Console.WriteLine("-------------------------------------");

        if (stackedBoxes != count)
        {
            List<Box> restOfTheBoxes = new List<Box>();
            for(int i = 0; i < numberOfHeaps; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (!heaps[numberOfHeaps - 1].Contains(sortedList[j]))
                    {
                        if (!restOfTheBoxes.Contains(sortedList[j])) restOfTheBoxes.Add(sortedList[j]);
                    }
                }
            }
            LeastNumberOfHeaps(restOfTheBoxes, restOfTheBoxes.Count, heaps, numberOfHeaps);
        }
    }

    public static void Main()
    {
    List<Box> list = new List<Box>();
    List<List<Box>> heaps = new List<List<Box>>();
    for (int i = 0; i < 10; i++)
    {
        Box newBox = new Box(new Random().Next(1, 10), new Random().Next(1, 10), new Random().Next(1, 10));
        list.Add(newBox);
        Console.WriteLine("{" + newBox.d + ", " + newBox.w + ", "+ newBox.h + "}" + "\n");
    }
    Console.WriteLine("Heighest posible heap is:" + MaxStackHeight(list, 10) + "\n");
    Console.WriteLine("Combination with minimum number of heaps ");
    LeastNumberOfHeaps(list, 10, heaps, 0);
    }
}