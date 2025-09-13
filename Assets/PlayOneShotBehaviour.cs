using UnityEngine;

public class PlayOneShotBehaviour : StateMachineBehaviour
{
    public AudioClip soundToPlay;
    [Tooltip("Âm lượng, có thể > 1 nếu muốn tăng cường độ")]
    public float volume = 1f;
    public bool playOnEnter = true, playOnExit = false, playAfterDelay = false;
    public bool stopWhenAnimationEnds = false; // 🔴 mới thêm

    public float playDelay = 0.25f;

    private float timeSinceEntered = 0f;
    private bool hasDelayedSoundPlayed = false;
    private AudioSource activeAudioSource; // lưu AudioSource hiện tại để stop nếu cần

    private void PlaySound(Animator animator)
    {
        if (soundToPlay == null) return;

        // Nếu đã có AudioSource cũ, xoá nó để tránh trùng
        if (activeAudioSource != null)
        {
            Object.Destroy(activeAudioSource);
        }

        // Tạo AudioSource tạm
        activeAudioSource = animator.gameObject.AddComponent<AudioSource>();
        activeAudioSource.clip = soundToPlay;
        activeAudioSource.volume = volume;
        activeAudioSource.spatialBlend = 0f; // 2D sound
        activeAudioSource.Play();

        if (!stopWhenAnimationEnds)
        {
            // Nếu không cần stop sớm → để nó tự hủy sau khi xong
            Object.Destroy(activeAudioSource, soundToPlay.length);
        }
    }

    // Khi state bắt đầu
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnEnter)
        {
            PlaySound(animator);
        }
        timeSinceEntered = 0f;
        hasDelayedSoundPlayed = false;
    }

    // Khi state đang chạy
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playAfterDelay && !hasDelayedSoundPlayed)
        {
            timeSinceEntered += Time.deltaTime;
            if (timeSinceEntered > playDelay)
            {
                PlaySound(animator);
                hasDelayedSoundPlayed = true;
            }
        }
    }

    // Khi state kết thúc
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnExit)
        {
            PlaySound(animator);
        }

        // 🔴 Nếu bật tùy chọn stopWhenAnimationEnds → dừng âm thanh ngay
        if (stopWhenAnimationEnds && activeAudioSource != null)
        {
            activeAudioSource.Stop();
            Object.Destroy(activeAudioSource);
            activeAudioSource = null;
        }
    }
}
