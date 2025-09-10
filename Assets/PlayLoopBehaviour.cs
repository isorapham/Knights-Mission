using UnityEngine;

public class PlayLoopBehaviour : StateMachineBehaviour
{
    public AudioClip soundToPlay;
    public float volume = 1f;

    private AudioSource audioSource;

    // Gọi khi animation state bắt đầu
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Nếu object chưa có AudioSource thì tạo mới
        audioSource = animator.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = animator.gameObject.AddComponent<AudioSource>();
        }

        // Cấu hình audio
        audioSource.clip = soundToPlay;
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.Play();
    }

    // Gọi khi animation state kết thúc
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.clip = null; // Giải phóng clip
        }
    }
}