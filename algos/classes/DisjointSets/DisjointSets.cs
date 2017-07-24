namespace classes
{
    /// <summary>
    /// Implementation of Disjoint sets data structure with tree height optimization
    /// ( https://en.wikipedia.org/wiki/Disjoint-set_data_structure ).
    /// Stores disjoint sets of N elements indexed with indicies from 0 to N-1 .
    /// Each set has an id - index of first added element.
    /// Used in:
    /// - Kruskal algorithm
    /// - Hackerrank HourRank 22 https://www.hackerrank.com/contests/hourrank-22/challenges/super-mancunian
    ///     (as part of finding minimal spanning tree and 
    ///     for support of check if last edge will complete MST). 
    /// </summary>
    public class DisjointSets
    {
        /// <summary>
        /// Links to parent elements.
        /// Each element of set belongs to set's tree and this is a links to parents elements in such tree.
        /// If element has no parent (i.e. is root) - corresponding element of array is equal to element.
        /// If element not belongs to any set - corresponding element of array is equal to -1.
        /// </summary>
        private int[] _parentLinkData;
        /// <summary>
        /// Tree height info.
        /// Tree of set elements must be as flat as possible, 
        /// so while union of subsets shorter tree should be attached to the root of longer tree.
        /// Doing that - height of longer tree will not increase.
        /// </summary>
        private int[] _treeHeightData;
        /// <summary>
        /// Overall count of sets.
        /// </summary>
        private int _setsCount = 0;

        /// <summary>
        /// Constructor, initializes structure with number of elements.
        /// </summary>
        /// <param name="n"></param>
        public DisjointSets(int n)
        {
            _parentLinkData = new int[n];
            _treeHeightData = new int[n];
            Reset();
        }

        /// <summary>
        /// Cleanup structure.
        /// </summary>
        public void Reset()
        {
            _setsCount = 0;
            for (int i = 0; i < _parentLinkData.Length; i++)
            {
                _parentLinkData[i] = -1;
                _treeHeightData[i] = 0;
            }
        }

        /// <summary>
        /// Get count of disjoint sets.
        /// </summary>
        public int SetsCount
        {
            get
            {
                return _setsCount;
            }
        }

        /// <summary>
        /// Create a set with one element of index X
        /// </summary>
        /// <param name="x">Index of first element</param>
        /// <returns>Id of created set</returns>
        public int CreateSet(int x)
        {
            _parentLinkData[x] = x;
            _treeHeightData[x] = 0;
            _setsCount++;
            return x;
        }

        /// <summary>
        /// Add element to set
        /// </summary>
        /// <param name="setId">Id of set</param>
        /// <param name="x">Element to add</param>
        public void AddToSet(int setId, int x)
        {
            _parentLinkData[x] = setId;
            if (_treeHeightData[setId] == 0)
            {
                _treeHeightData[setId] = 1;
            }
        }

        /// <summary>
        /// Get id of set which contains element
        /// </summary>
        /// <param name="x">Index of element</param>
        /// <returns>Id of set or -1 if element not included in any set</returns>
        public int GetIncludingSetId(int x)
        {
            if (_parentLinkData[x] == -1)
            {
                return -1;
            }
            var currParent = _parentLinkData[x];
            while (currParent != _parentLinkData[currParent])
            {
                currParent = _parentLinkData[currParent];
            }
            return currParent;
        }

        /// <summary>
        /// Union two sets
        /// </summary>
        /// <param name="setId1">Id of set 1</param>
        /// <param name="setId2">Id of set 2</param>
        public void Union(int setId1, int setId2)
        {
            // try to attach shorter tree to root of longer one
            if (_treeHeightData[setId1] < _treeHeightData[setId2])
            {
                _parentLinkData[setId1] = setId2;
            }
            else if (_treeHeightData[setId1] > _treeHeightData[setId2])
            {
                _parentLinkData[setId2] = setId1;
            }
            // heights of trees are equel - attach 2nd tree to root of 1st
            else
            {
                _parentLinkData[setId2] = setId1;
                _treeHeightData[setId1]++;
            }
            _setsCount--;
        }
    }

}
