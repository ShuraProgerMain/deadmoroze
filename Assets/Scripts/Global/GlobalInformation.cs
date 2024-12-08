using UnityEngine;

namespace Global
{
    public class GlobalInformation : MonoBehaviour
    {
        public static GlobalInformation init;

        public AnimationCurve unitsSpawn;
        public AnimationCurve damageLevelCOVID;
        public AnimationCurve damageLevelSnowman;

        public int currentMinutes = 0;

        #region Events

        public delegate void TimeUpdate(int i);

        public TimeUpdate UnitsSpawnChanges;
        public TimeUpdate COVIDMainDamageChange;
        public TimeUpdate SnowmanDamageChange;

        #endregion

        private void Awake() 
        {
            if(!init)
            {
                init = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void UpdateCurrentMinutes(int value)
        {
            currentMinutes = value;
            UpdateInformationTime();
        }

        private void UpdateInformationTime()
        {
            UnitsSpawnChanges?.Invoke((int)unitsSpawn.Evaluate(currentMinutes));
            COVIDMainDamageChange?.Invoke((int)damageLevelCOVID.Evaluate(currentMinutes));
            SnowmanDamageChange?.Invoke((int)damageLevelSnowman.Evaluate(currentMinutes));
        }

    }
}
