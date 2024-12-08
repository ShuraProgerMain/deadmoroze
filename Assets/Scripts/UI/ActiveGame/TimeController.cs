using System.Collections;
using Global;
using TMPro;
using UnityEngine;

namespace UI.ActiveGame
{
    public class TimeController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _minutesText;
        [SerializeField] private TextMeshProUGUI _secondsText;


        void Start()
        {
            StartCoroutine(TimeCounter());
        }

        private IEnumerator TimeCounter()
        {
            var seconds = 0;
            var minutes = 0;
            while(true)
            {
                yield return new WaitForSeconds(1f);
                UpdateUI(minutes, seconds);

                if((seconds + 1) == 60)
                {
                    seconds = 0;
                    minutes++;
                    GlobalInformation.init.UpdateCurrentMinutes(minutes);
                }
                else
                {
                    seconds++;
                }
            }
        }

        private void UpdateUI(int minutes, int seconds)
        {
            _minutesText.text = minutes.ToString();
            _secondsText.text = ": " + seconds.ToString();
        }
    }
}
