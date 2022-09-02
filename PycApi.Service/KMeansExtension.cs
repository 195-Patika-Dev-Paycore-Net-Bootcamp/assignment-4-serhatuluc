using System;


namespace PycApi
{
    public static class KMeansExtension
    {
        public static int[] Clusterize(this double[][] data, int numOfClusters)
        {
            bool changed = true; bool success = true;
            //Assign random cluster to the points 
            int[] clustering = randomlyAssign(data.Length, numOfClusters,0);

            //Creating list of means 
            double[][] means = CreateListMeans(numOfClusters, data[0].Length);

            //Looping length*10 times
            int maxCount = data.Length * 10;
            int ct = 0;
            while (changed == true && success == true && ct < maxCount)
            {
                ++ct;
                //Updating means everytime
                success = UpdateMeans(data, clustering, means);
                //Updating clustering everytime
                changed = UpdateClustering(data, clustering, means);
            }
            return clustering;
        }


        public static int[] randomlyAssign(int lengthData, int numClusters,int seed)
        {
            //This method will assign each location to a random cluster
            Random random = new Random(seed);

            //This list will have the number of cluster for each location
            //If three cluster are requested. List seems like this -> [0,1,1,2,1,2,2,1]
            //0->First cluster
            //1->Second cluster
            //2->Third cluster
            int[] clustering = new int[lengthData];
            for (int i = 0; i < numClusters; ++i)
                clustering[i] = i;
            for (int i = numClusters; i < clustering.Length; ++i)
                clustering[i] = random.Next(0, numClusters);
            return clustering;
        }

        private static double[][] CreateListMeans(int numClusters, int numColumns)
        {
            //List is created to hold the means of each clusters
            double[][] result = new double[numClusters][];

            for (int k = 0; k < numClusters; ++k)//The length of the list will be the number of cluster is requested
                result[k] = new double[numColumns]; //numColumnns is 2. Since we have x and y coordinates.
                                                    //So there will be mean of x and y for each cluster
            return result;
        }

        private static bool UpdateMeans(double[][] data, int[] clustering, double[][] means)
        {
            //Clusters number is equal to length of means
            int numClusters = means.Length;
            //Number of location that each cluster is counted
            int[] clusterCounts = new int[numClusters];

            //Counting is done here
            for (int i = 0; i < data.Length; ++i)
            {
                int cluster = clustering[i];
                ++clusterCounts[cluster];
            }

            //If any cluster does not have any location return false.
            //Since randomlyAssign method did not assign all clusters
            for (int k = 0; k < numClusters; ++k)
                if (clusterCounts[k] == 0)
                    return false;

            //Means are assigned to 0.0
            for (int k = 0; k < means.Length; ++k)
                for (int j = 0; j < means[k].Length; ++j)
                    means[k][j] = 0.0;

            //Each x and y for all locations tthat belongs to each cluster is calculated
            for (int i = 0; i < data.Length; ++i)
            {
                int cluster = clustering[i];//This line will determine which cluster the location belongs
                for (int j = 0; j < data[i].Length; ++j)
                    means[cluster][j] += data[i][j]; // Accumulating sum
            }

            for (int k = 0; k < means.Length; ++k)
                for (int j = 0; j < means[k].Length; ++j)
                    means[k][j] /= clusterCounts[k]; //Here cluster count is used to divide sum with the number of locations
            return true;
        }

        private static bool UpdateClustering(double[][] data, int[] clustering, double[][] means)
        {
            //Clusters number is equal to length of means
            int numClusters = means.Length;
            bool changed = false;

            //New List for clustering is created
            int[] newClustering = new int[clustering.Length];

            //Clustering is copied to newClustering
            Array.Copy(clustering, newClustering, clustering.Length);

            //Distance to each means of cluster will be held in distances
            double[] distances = new double[numClusters];

            for (int i = 0; i < data.Length; ++i)
            {
                //Each distance to each cluster is calculated
                for (int k = 0; k < numClusters; ++k)
                    distances[k] = Distance(data[i], means[k]);

                //Minimum distance is found and location is inserted to new cluster
                int newClusterID = MinIndex(distances);
                if (newClusterID != newClustering[i]) //If cluster is changed for location new clustering is changed
                {
                    changed = true;
                    newClustering[i] = newClusterID;
                }
            }
            //If no change is made then return false
            if (changed == false)
                return false;

            //Cluster counts are done again to update
            int[] clusterCounts = new int[numClusters];
            for (int i = 0; i < data.Length; ++i)
            {
                int cluster = newClustering[i];
                ++clusterCounts[cluster];
            }

            //If any cluster is empty return false
            for (int k = 0; k < numClusters; ++k)
                if (clusterCounts[k] == 0)
                    return false;

            //newclustering is copied to clustering
            Array.Copy(newClustering, clustering, newClustering.Length);
            return true; // no zero-counts and at least one change
        }

        private static double Distance(double[] tuple, double[] mean)
        {
            //Distance to the mean of cluster is calculated
            double sumSquaredDiffs = 0.0;
            for (int j = 0; j < tuple.Length; ++j)
                //tuple - mean is the distance
                sumSquaredDiffs += Math.Pow((tuple[j] - mean[j]), 2);
            return Math.Sqrt(sumSquaredDiffs);
        }

        private static int MinIndex(double[] distances)
        {
            //minimum distance to a mean of cluster is found
            int indexOfMin = 0;
            double smallDist = distances[0];
            for (int k = 0; k < distances.Length; ++k)
            {
                if (distances[k] < smallDist)
                {
                    smallDist = distances[k];
                    indexOfMin = k; //Then index for minimum distance is found
                }
            }
            return indexOfMin;
        }
    }
}
