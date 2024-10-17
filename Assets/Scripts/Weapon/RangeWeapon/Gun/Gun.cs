
using UnityEngine;
using System.Collections;


public class Gun : AbstractWeapon
{
    protected bool isRecoiling = false;
    protected Vector3 originalPosition;
    protected Vector3 recoilTargetPosition;

    public override void Attack() {
        Debug.Log("Shooting");
    }

    public virtual new void  Start()
    {
        base.Start();
        originalPosition = transform.localPosition;
    }

    protected  IEnumerator Recoil(float recoilDistance, float recoilDuration, Transform firePoint)
    {
        if (isRecoiling)
            yield break;

        isRecoiling = true;

        // Tính toán hướng giật
        Vector3 recoilDirection = -firePoint.right.normalized;
        recoilTargetPosition = originalPosition + recoilDirection * recoilDistance;

        float elapsedTime = 0f;

        // Giật về phía trước
        while (elapsedTime < recoilDuration)
        {
            transform.localPosition = Vector2.Lerp(originalPosition, recoilTargetPosition, elapsedTime / recoilDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //  Trở lại vị trí ban đầu
        elapsedTime = 0f;
        while (elapsedTime < recoilDuration)
        {
            transform.localPosition = Vector2.Lerp(recoilTargetPosition, originalPosition, elapsedTime / recoilDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
        isRecoiling = false;
    }

}
