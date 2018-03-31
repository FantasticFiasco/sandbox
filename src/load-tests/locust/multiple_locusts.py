# Run load test with the following command:
# locust --host=http://localhost:3000 -f ./multiple_locusts.py DesktopUser MobileUser

from locust import HttpLocust, TaskSet, task

class Behavior(TaskSet):
    @task
    def index(self):
        self.client.get("/")

    @task
    def profile(self):
        self.client.get('/profile')

class DesktopUser(HttpLocust):
    task_set = Behavior
    weight = 1
    min_wait = 1000   # 1s
    max_wait = 5000   # 5s

class MobileUser(HttpLocust):
    task_set = Behavior
    weight = 3        # Simulate 3x traffic from mobile
    min_wait = 3000   # 3s
    max_wait = 7000   # 7s