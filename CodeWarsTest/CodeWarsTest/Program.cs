int[] nums = { 5, 10, 86, 656, 543, 8 };
int[] newNums = FindSmaller(nums);
for(int i = 0; i < newNums.Length; i++)
{
    Console.WriteLine(newNums[i]);
}

int[] FindSmaller(int[] nums)
{
    int temp = 0;
    for(int write = 0; write < nums.Length; write++)
    {
        for(int sort = 0; sort < nums.Length-1; sort++)
        {
            if (nums[sort]> nums[sort+1])
            {
                temp = nums[sort+1];
                nums[sort+1] = nums[sort];
                nums[sort] = temp;
            }
        }
    }
    return nums;
}
//int[] num = { 1, 3, 5, 7, 9 };
//Console.WriteLine(binarySearch(num, 9));
//Console.WriteLine(binarySearch(num, -1));

//int binarySearch(int[] num,int item)
//{
//    int low = 0;
//    int high = num.Length -1;
//    while (low <= high)
//    {
//        int mid = (low + high) >> 1;
//        int guess = num[mid];
//        if(guess == item)
//        {
//            return mid;
//        }
//        if(guess > item)
//        {
//            high = mid - 1;
//        }
//        else
//        {
//            low = mid + 1;
//        }
//    }
//    return low;
//}


//HumanTimeFormat.formatDuration(8684256);
//public class HumanTimeFormat
//{
//    public static void formatDuration(int seconds)
//    {
//        var ts = TimeSpan.FromSeconds(seconds);
//        if (seconds == 0) Console.WriteLine("now");
//        Console.WriteLine("{0} д. {1} ч. {2} м. {3} с. {4} мс.", ts.Days, ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
//    }
//}
