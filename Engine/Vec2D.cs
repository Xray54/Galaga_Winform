using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    /// <summary>
    /// x,y 만 가지고 있는 벡터
    /// </summary>
    public struct Vec2D
    {
        public Vec2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; set; }
        public float Y { get; set; }
        /// <summary>
        /// 벡터의 크기를 반환
        /// </summary>
        /// <returns>벡터의 크기</returns>
        public float magnitude()
        {
            return Convert.ToSingle(Math.Sqrt(X * X + Y * Y));
        }
        /// <summary>
        /// 벡터를 정규화 시킴
        /// </summary>
        /// <returns>정규화 시킨 벡터</returns>
        public Vec2D normalized()
        { 
            return this / magnitude();
        }
        
        public override bool Equals(object obj)
        {
            if (obj is Vec2D)
            {
                return this.Equals((Vec2D)obj);
            }
            return false;
        }
        public bool Equals(Vec2D p)
        {
            return (X == p.X) && (Y == p.Y);
        }
        public override int GetHashCode()
        {
            int result = Convert.ToInt32(X);
            result = 32 * result + Convert.ToInt32(Y);
            return result;
        }
        public override string ToString()
        {
            return string.Format($"{X},{Y}");
        }
        public static Vec2D operator *(Vec2D a, float scalar)
        {
            return new Vec2D(a.X * scalar, a.Y * scalar);
        }        
        public static Vec2D operator *(float scalar, Vec2D a)
        {
            return new Vec2D(a.X * scalar, a.Y * scalar);
        }
        public static Vec2D operator /(Vec2D a, float scalar)
        {
            return new Vec2D(a.X / scalar, a.Y / scalar);
        }        
        public static Vec2D operator /(float scalar, Vec2D a)
        {
            return new Vec2D(a.X / scalar, a.Y / scalar);
        }
        public static Vec2D operator +(Vec2D left, Vec2D right)
        {
            return new Vec2D(left.X + right.X, left.Y + right.Y);
        }
        public static Vec2D operator -(Vec2D left, Vec2D right)
        {
            return new Vec2D(left.X - right.X, left.Y - right.Y);
        }
        /// <summary>
        /// 벡터의 크기 비교
        /// </summary>
        /// <param name="left">비교할 벡터1</param>
        /// <param name="right">비교할 벡터2</param>
        /// <returns>대소 비교 반환</returns>
        public static bool operator <(Vec2D left, Vec2D right)
        {
            return left.magnitude() < right.magnitude();
        }
        public static bool operator >(Vec2D left, Vec2D right)
        {
            return left.magnitude() > right.magnitude();
        }
        public static bool operator ==(Vec2D left, Vec2D right)
        {
            return left.X == right.X && left.Y == right.Y;
        }
        public static bool operator !=(Vec2D left, Vec2D right)
        {
            return !(left == right);
        }
    }
}
