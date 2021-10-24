namespace Pinterest.Models
{
    public class Following
    {
        public User Follower { get; set; }
        public int FollowerId { get; set; }
        
        public Board Followee { get; set; }
        public int FolloweeId { get; set; }
        
        private Following() {}

        public Following(int followerId, int followeeId)
        {
            FollowerId = followerId;
            FolloweeId = followeeId;
        }
    }
}